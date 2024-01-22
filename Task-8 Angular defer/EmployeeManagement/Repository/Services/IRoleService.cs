using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Repository.Services
{
    public interface IRoleService
    {
        Task<HttpStatusCode> NewRole(Roles role);
        Task<HttpStatusCode> UpdateRole(Roles role);
        Task<HttpStatusCode> RemoveRole(int id);
        Task<object> ViewRoles();
        Task<object> GetRole(int id);
    }
}
