using Core.Application.DTOS.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Inferfaces.Service
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> Authentication(AuthenticationRequest request);
        Task<string> ConfirmEmail(string userId, string token);
        Task<GenericResponse> ForgotPassword(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> Register(RegisterRequest request, string origin);
        Task<GenericResponse> UpdateUser(string userId, RegisterRequest request);
        Task<GenericResponse> ResetPassword(ResetPasswordRequest request);
        List<AuthenticationResponse> GetAllAgents();
        Task<AuthenticationResponse> GetUserInfo(string Id);
        Task ChangeUserState(string id, string estado);
        Task SignOut();
    }
}