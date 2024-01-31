using MediatorLibrary.Repository.Models;
using MediatorLibrary.Repository.Queries;
using MediatorLibrary.Repository.Services;
using MediatR;

namespace MediatorLibrary.Repository.Handler
{
    public class ViewEmployeeListHandler : IRequestHandler<ViewEmployeeListQuery, IEnumerable<Employee>>
    {
        private readonly IDataRepository dataRepository;

        public ViewEmployeeListHandler(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public Task<IEnumerable<Employee>> Handle(ViewEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.dataRepository.getEmployees());
        }
    }
}