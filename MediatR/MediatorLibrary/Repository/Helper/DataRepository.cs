using MediatorLibrary.Repository.Models;
using MediatorLibrary.Repository.Services;

namespace MediatorLibrary.Repository.Helper
{
    public class DataRepository : IDataRepository
    {
        private static List<Employee> EmployeeDetails = new List<Employee>()
        {
            new Employee{
                EmailId = "employee1@gmail.com",
                Id = Guid.NewGuid(),
                Name = "Subramani",
                MobileNumber = "1234567890" 
            },
            new Employee{
                EmailId = "employee2@gmail.com",
                Id = Guid.NewGuid(),
                Name = "Arun",
                MobileNumber = "1234567890" 
            }
        };

        public Employee getEmployee(Guid id)
        {
            return EmployeeDetails.Where(employee => employee.Id == id).FirstOrDefault(new Employee());
        }

        public IEnumerable<Employee> getEmployees()
        {
            return EmployeeDetails;
        }
    }
}

