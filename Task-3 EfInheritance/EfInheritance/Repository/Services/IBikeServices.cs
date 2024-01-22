using System.Net;
using EfInheritance.Repository.DbModels;

namespace EfInheritance.Repository.Services
{
    public interface IBikeServices
    {
        Task<HttpStatusCode> addBikeDetails(Bike bikeDetails);
        Task<object> viewBikeDetails();
    }
}