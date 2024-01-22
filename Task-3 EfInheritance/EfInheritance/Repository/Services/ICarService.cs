using System.Net;
using EfInheritance.Repository.DbModels;

namespace EfInheritance.Repository.Services
{
    public interface ICarService
    {
        Task<HttpStatusCode> addCarDetails(Car carDetails);
        Task<object> viewCarDetails();
    }
}