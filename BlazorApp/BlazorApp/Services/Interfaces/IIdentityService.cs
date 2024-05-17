using BlazorApp.Extensions.ViewModels;
using System.Security.Claims;

namespace BlazorApp.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<ClaimsPrincipal> SignInAsync(UserSignInViewModel viewModel);
        Task SignInWithGoogleAsync();
        Task<JwtViewModel> SignUpAsync(UserSignUpViewModel viewModel);
        Task SingOutAsync();
    }
}
