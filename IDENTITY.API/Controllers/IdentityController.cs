
using FluentValidation;
using Google.Apis.Auth;
using IDENTITY.BLL.Configurations;
using IDENTITY.BLL.DTO.Requests;
using IDENTITY.BLL.DTO.Responses;
using IDENTITY.BLL.Services.Interfaces;
using IDENTITY.DAL.Entities;
using IDENTITY.DAL.Exceptions;
using MassTransit.Util.Scanning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IDENTITY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IValidator<UserSignUpRequest> _SingUpValidator;
        private readonly IValidator<UserSignInRequest> _SingInValidator;
        private readonly IIdentityService _IdentityService;
        private readonly GoogleClientConfiguration googleClientConfiguration;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<IdentityController> logger;
        private readonly ITokenService tokenService;

        public IdentityController(IValidator<UserSignInRequest> singinvalidator, IValidator<UserSignUpRequest> singupvalidator, IIdentityService identityService, GoogleClientConfiguration googleClientConfiguration, UserManager<User> userService, ITokenService tokenService, ILogger<IdentityController> logger, SignInManager<User> signInManager)
        {
            _SingInValidator = singinvalidator;
            _SingUpValidator = singupvalidator;
            _IdentityService = identityService;
            this.googleClientConfiguration = googleClientConfiguration;
            userManager = userService;
            this.tokenService = tokenService;
            this.logger = logger;
            this.signInManager = signInManager;
        }


        [HttpPost("signUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtResponse>> SignUpAsync(
       [FromBody] UserSignUpRequest request)
        {
            try
            {
                if (request == null) { throw new ArgumentNullException(nameof(request)); }
                var refererUrl = HttpContext.Request.Headers["Referer"].ToString();
                var uri = new Uri(refererUrl);
                var baseUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}";
                request.refererUrl = baseUrl;
                var valid = _SingUpValidator.Validate(request);
                if (!valid.IsValid) { throw new ValidationException(valid.Errors); }

                var response = await _IdentityService.SignUpAsync(request);

                HttpContext.Response.Cookies.Append("Bearer", response.Token, new()
                {
                    Expires = DateTime.Now.AddDays(2),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
                return Ok(response);
            }
            catch (EmailNotConfirmedException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }




        [HttpPost("signIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtResponse>> SignInAsync(
            [FromBody] UserSignInRequest request)
        {
            try
            {
                if (request == null) { throw new ArgumentNullException(nameof(request)); }
                var refererUrl = HttpContext.Request.Headers.Referer.ToString();
                var uri = new Uri(refererUrl) ;
                var baseUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}";
                request.refererUrl = baseUrl;
                var valid = _SingInValidator.Validate(request);

                if (!valid.IsValid) { throw new ValidationException(valid.Errors); }

                var response = await _IdentityService.SignInAsync(request);

                HttpContext.Response.Cookies.Append("Bearer", response.Token, new()
                {
                    Expires = DateTime.Now.AddDays(2),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(response.Token);
        
                HttpContext.User = new(GenerateStateFromToken(jwtToken));



                return Ok(response);
            }
            catch (EmailNotConfirmedException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        private static ClaimsPrincipal GenerateStateFromToken(JwtSecurityToken token)
                {
                    var identity = new ClaimsIdentity(token.Claims, "apiauth_type");
                    var principal = new ClaimsPrincipal(identity);
                    return principal;
                }



        [HttpPost("LoginWithGoogle")]
        public async Task<ActionResult<JwtResponse>> LoginWithGoogle([FromBody] string credentials)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { googleClientConfiguration.GoogleClientID }


            };


            var peyload = await GoogleJsonWebSignature.ValidateAsync(credentials, settings);


            var user = userManager.FindByEmailAsync(email: peyload.Email).Result;

            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "user not found");
            }
            var jwtToken = tokenService.BuildToken(user);
            HttpContext.Response.Cookies.Append("Bearer", tokenService.SerializeToken(jwtToken), new()
            {
                Expires = DateTime.Now.AddDays(2),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });
            return Ok(new JwtResponse() { Id = user.Id, Token = tokenService.SerializeToken(jwtToken), UserName = user.UserName });

        }

        [HttpPost("SignInWitGoogleAsync")]
        public async Task<IActionResult> SignInWitGoogleAsync()
        {
            try
            {
                await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                    new AuthenticationProperties { RedirectUri = Url.Action("GoogleResopnse") });
                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }


        [HttpPost("GoogleResopnse")]
        public async Task<IActionResult> GoogleResopnse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (result != null)
                {
                    var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim =>
                    new
                    {
                        claim.Issuer,
                        claim.OriginalIssuer,
                        claim.Type,
                        claim.Value
                    });

                    return Ok(claims);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }







        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            try
            {
                if (request == null) { throw new ArgumentNullException(nameof(request)); }


                await _IdentityService.ConfirmEmail(request);
                return Ok();
            }
 
            catch (EmailNotConfirmedException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // схеми автентифікації
        [HttpGet("GetExternalAuthenticationSchemes")]
        public async Task<IActionResult> GetExternalAuthenticationSchemesAsync()
        {
            try
            {
                var result  = await signInManager.GetExternalAuthenticationSchemesAsync();
                if (result == null) throw new Exception("No External Authentication Schemes");

                return Ok(result.Select(p=>p.DisplayName).ToList());
            
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("RefreshSignIn")]
        public async Task<IActionResult> RefreshSignInAsync(Guid id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id.ToString());
                if (user == null) return Unauthorized();

                await signInManager.RefreshSignInAsync(user);
                var jwtToken = tokenService.BuildToken(user);
                HttpContext.Response.Cookies.Append("Bearer", tokenService.SerializeToken(jwtToken), new()
                {
                    Expires = DateTime.Now.AddDays(2),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetExternalLoginInfo")]
        public async Task<IActionResult> GetExternalLoginInfoAsync()
        {
            try
            {
                var result = await signInManager.GetExternalLoginInfoAsync();
                if (result == null) throw new Exception("No External Login Info");

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ResendConfirmationEmail")]
        public async Task<IActionResult> ResendConfirmationEmailAsync([FromBody]string Email)
        {
            try
            {
                var refererUrl = HttpContext.Request.Headers["Referer"].ToString();
                var uri = new Uri(refererUrl);
                var baseUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}";
                var user = await userManager.FindByEmailAsync(Email);
                await _IdentityService.SendEmailConfirmation(user.Id, baseUrl);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex);
            }


        }





    }
}
