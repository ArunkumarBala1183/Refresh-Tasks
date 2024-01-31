using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Repository.Services
{
    public interface IAuthenticateService
    {
        Task<object> AuthenticateCredentials(UserCredentialsDto credentials , bool isOtpVerified);

        Task<object> GenerateNewToken(Token token);
    }
}
