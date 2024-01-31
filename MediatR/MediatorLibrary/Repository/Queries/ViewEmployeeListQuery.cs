using MediatorLibrary.Repository.Models;
using MediatR;

namespace MediatorLibrary.Repository.Queries
{
    public record ViewEmployeeListQuery() : IRequest<IEnumerable<Employee>>;
}