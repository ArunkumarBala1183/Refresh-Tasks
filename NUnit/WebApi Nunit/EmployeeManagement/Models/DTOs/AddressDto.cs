using EmployeeManagement.Models.DbModels;

namespace EmployeeManagement.Models.DTOs
{
    public record struct AddressDto
    (
        int Id,
        string DoorNumber,
        string Street,
        string City,
        string District,
        string State,
        string PostalCode,
        int EmployeeId
    );
}
