using EmployeeManagement.Models.DbModels;

namespace EmployeeManagement.Models.DTOs
{
    public record struct RoleDto 
        (
        int Id,
        string RoleCode,
        string Role,
        List<EmployeeRoleDto> EmployeeRoles
        );
}
