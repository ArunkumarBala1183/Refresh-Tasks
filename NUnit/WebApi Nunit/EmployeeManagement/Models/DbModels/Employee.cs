using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.DbModels
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateOnly DateofBirth { get; set; }
        public string EmailId{ get; set; }
        public string MobileNumber { get; set; }

        public Address Address { get; set; }

        //public int? RoleId { get; set; }
        //public Roles Role { get; set; }

        public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

        public UserCredentials UserCredentials { get; set; }

    }
}
