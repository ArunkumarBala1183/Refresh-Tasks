using MediatorLibrary.Repository.Helper;
using MediatR;

namespace MediatorLibrary.Repository.Queries
{
    public record GetEmployeeQuery() : IRequest<DataRepository>;
}