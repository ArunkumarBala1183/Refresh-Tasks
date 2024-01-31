using EmployeeManagement.Models.DbModels;

namespace EmployeeManagement.Models.DTOs
{
    //public class EmployeeDto
    //{
    //    public int EmployeeId { get; set; }
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //    public string EmailId { get; set; }
    //    public string MobileNumber { get; set; }
    //    public int? RoleId { get; set; }
    //    public AddressDto Address { get; set; }
    //}

    public record struct EmployeeDto(
        int EmployeeId, 
        string Name, 
        int Age, 
        DateOnly DateofBirth,
        string EmailId, 
        string MobileNumber,
        int RoleId,
        AddressDto Address,
        List<EmployeeRoleDto>? EmployeeRoles
        );
}
