namespace EmployeeManagement.Models.Application_Models
{
    public class ApiResponse
    {
        public int HttpStatusCode { get; set; }
        public string Status { get; set; }
        public bool IsError { get; set; }
    }
}
