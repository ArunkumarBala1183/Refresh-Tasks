using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Repository.Services
{
    public interface IEmployeeService
    {
        Task<ApiResponse> NewEmployee(EmployeeDto employeeDetails);
        Task<ApiResponse> UpdateEmployee(EmployeeDto employeeDetails);
        Task<ApiResponse> DeleteEmployee(int id);
        Task<object> ViewEmployees();
        Task<object> GetEmployee(int id);
        Task<ApiResponse> RoleUpdate(int id, List<EmployeeRoleDto> roles);
    }
}
