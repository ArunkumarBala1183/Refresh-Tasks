using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.DbModels
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string RoleCode { get; set; }
        public string Role { get; set; }

        public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

    }
}
