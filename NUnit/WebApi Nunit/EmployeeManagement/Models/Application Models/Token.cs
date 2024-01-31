namespace EmployeeManagement.Models.Application_Models
{
    public class Token
    {
        public string CurrentToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
