using BlazorApp.Extensions;
using BlazorApp.Extensions.ViewModels;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace BlazorApp.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApiHttpClient httpClient;

        public IdentityService(IHttpClientFactory clientFactory, ApiHttpClient httpClient,HttpClient client)
        {

            this.httpClient = httpClient.SetHttpclient(client); 

        }
        public async Task<ClaimsPrincipal> SignInAsync(UserSignInViewModel viewModel)
        {

            var response = await httpClient.PostAsync<UserSignInViewModel, JwtViewModel>("signIn", viewModel);

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(response.token);
            var state = GenerateStateFromToken(jwtToken);




            return await state;
        }
        private async Task<ClaimsPrincipal> GenerateStateFromToken(JwtSecurityToken token)
        {
            var identity = new ClaimsIdentity(token.Claims, "apiauth_type");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
        public async Task<JwtViewModel> SignUpAsync(UserSignUpViewModel viewModel) =>
            await ExecuteRequestAsync("signUp", viewModel);

        public async Task SignInWithGoogleAsync() =>
       await ExecuteAsync("SignInWitGoogleAsync");


        private async Task<JwtViewModel> ExecuteRequestAsync<T>(string requestUri, T? model)
        {
            var jwtModel = await httpClient.PostWithoutAuthorizationAsync<T, JwtViewModel>(
                requestUri,
                model);

            return jwtModel;
        }
        private async Task ExecuteAsync(string requestUri)
        {
            await httpClient.PostAsync(requestUri);

        }
        public async Task SingOutAsync()
        {
        }






    }
}
