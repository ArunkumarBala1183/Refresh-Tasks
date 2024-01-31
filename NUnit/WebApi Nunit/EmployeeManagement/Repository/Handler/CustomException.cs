namespace EmployeeManagement.Repository.Handler
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
        
    }
}
