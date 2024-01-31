using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using System.Net;

namespace EmployeeManagement.Repository.Services
{
    public interface IAuthenticateService
    {
        Task<object> AuthenticateCredentials(UserCredentialsDto credentials);

        Task<object> GenerateNewToken(Token token);
    }
}
