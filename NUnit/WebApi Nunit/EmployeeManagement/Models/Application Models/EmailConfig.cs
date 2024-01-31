namespace EmployeeManagement.Models.Application_Models
{
    public class EmailConfig
    {
        public string? Sender { get; set; }

        public string? Password { get; set; }

        public string? Host { get; set; }
        public string? DisplayName { get; set; }

        public int Port { get; set; }
    }
}
