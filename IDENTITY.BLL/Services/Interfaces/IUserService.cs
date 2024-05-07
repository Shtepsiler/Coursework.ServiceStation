using IDENTITY.BLL.DTO.Requests;
using IDENTITY.BLL.DTO.Responses;

namespace IDENTITY.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetClientById(Guid Id);
        Task UpdateAsync(Guid Id, UserRequest client);
        Task DeleteAsync(Guid Id);
        Task ResetPassword(ResetPasswordRequest request);
        Task ForgotPassword(ForgotPasswordRequest request);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    }
}
