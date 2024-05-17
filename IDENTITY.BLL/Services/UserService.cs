using AutoMapper;
using IDENTITY.BLL.Configurations;
using IDENTITY.BLL.DTO.Requests;
using IDENTITY.BLL.DTO.Responses;
using IDENTITY.BLL.Services.Interfaces;
using IDENTITY.DAL.Data;
using IDENTITY.DAL.Entities;
using IDENTITY.DAL.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IDENTITY.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;
        private readonly ClientAppConfiguration client;
        private readonly AppDBContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly EmailSender emailSender;
        private readonly RoleManager<Role> roleManager;

        public UserService(IMapper mapper, ITokenService tokenService, AppDBContext dbContext, UserManager<User> userManager, EmailSender emailSender, IConfiguration configuration, ClientAppConfiguration clientAppConfiguration, RoleManager<Role> roleManager)
        {
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.configuration = configuration;
            client = clientAppConfiguration;
            this.roleManager = roleManager;
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            try
            {

                var user = await userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    throw new EntityNotFoundException("User not found");
                }
                if (!(await userManager.IsEmailConfirmedAsync(user)))
                {
                    throw new EmailNotConfirmedException("Email not confirmed");  
                }
                var result = await userManager.ResetPasswordAsync(user, request.Code, request.NewPasword);

                if (!result.Succeeded)
                {
                    throw new ArgumentException(string.Join("\n",
                        result.Errors.Select(error => error.Description)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
                if (user == null)
                    throw new EntityNotFoundException("User not found");
                if (!(await userManager.IsEmailConfirmedAsync(user)))
                {
                    throw new EmailNotConfirmedException("Email not confirmed");
                }
                var code = await userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

                var callbackUrl = $"{client.Url}{client.ResetPasswordPath}?Id={user.Id}&Code={code}";
                await emailSender.SendEmailAsync(request.Email, "Reset password", callbackUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            try
            {
                var users = await userManager.Users.ToListAsync();


                return mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<UserResponse> GetUserById(Guid Id)
        {
            try
            {
                string id = Id.ToString();
                var user = await userManager.FindByIdAsync(id);

                if (user == null) throw new Exception();

                return mapper.Map<UserResponse>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(Guid Id, UserRequest client)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id.ToString());

                if (user == null) throw new Exception();

                user.UserName = client.UserName;
                user.PhoneNumber = client.PhoneNumber;
                user.Email = client.Email;



                await userManager.UpdateAsync(user);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(Guid Id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id.ToString());
                if (user == null) throw new Exception();

                await userManager.DeleteAsync(user);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
