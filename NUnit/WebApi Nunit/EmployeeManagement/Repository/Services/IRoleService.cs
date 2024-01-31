using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Repository.Services
{
    public interface IRoleService
    {
        Task<HttpStatusCode> NewRole(RoleDto roleDetails);
        Task<HttpStatusCode> UpdateRole(RoleDto roleDetails);
        Task<HttpStatusCode> RemoveRole(int id);
        Task<object> ViewRoles();
        Task<object> GetRole(int id);
    }
}
