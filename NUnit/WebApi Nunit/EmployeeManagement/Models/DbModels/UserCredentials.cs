namespace EmployeeManagement.Models.DbModels
{
    public class UserCredentials
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
