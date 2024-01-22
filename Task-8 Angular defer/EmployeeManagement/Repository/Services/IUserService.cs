using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using System.Net;

namespace EmployeeManagement.Repository.Services
{
    public interface IUserService
    {
        Task<HttpStatusCode> AddUser(UserCredentials credentials);
        Task<HttpStatusCode> DeleteUser(int id);
    }
}
