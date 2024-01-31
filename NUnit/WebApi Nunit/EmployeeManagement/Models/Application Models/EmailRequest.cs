namespace EmployeeManagement.Models.Application_Models
{
    public class EmailRequest
    {
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
