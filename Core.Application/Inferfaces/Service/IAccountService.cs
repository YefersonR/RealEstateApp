using Core.Application.DTOS.Account;
using System.Threading.Tasks;

namespace Core.Application.Inferfaces.Service
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> Authentication(AuthenticationRequest request);
        Task<string> ConfirmEmail(string userId, string token);
        Task<GenericResponse> ForgotPassword(ForgotPasswordRequest request, string origin);
        Task<GenericResponse> RegisterClient(RegisterRequest request, string origin);
        Task<GenericResponse> ResetPassword(ResetPasswordRequest request);
        Task SignOut();
    }
}