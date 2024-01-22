namespace EmployeeManagement.Models.DTOs
{
    public record struct EmployeeRoleDto(
        int Id,
        int EmployeeId,
        int RoleId
        );
}
