using MediatorLibrary.Repository.Models;

namespace MediatorLibrary.Repository.Services
{
    public interface IDataRepository
    {
        IEnumerable<Employee> getEmployees();

        Employee getEmployee(Guid id);
        
    }
}