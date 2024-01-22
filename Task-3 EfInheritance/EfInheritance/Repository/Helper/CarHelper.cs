using System.Net;
using EfInheritance.Repository.DatabaseContext;
using EfInheritance.Repository.DbModels;
using EfInheritance.Repository.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EfInheritance.Repository.Helper
{
    public class CarHelper : ICarService
    {
        private readonly InheritanceContext database;
        public CarHelper(InheritanceContext context)
        {
            this.database = context;
        }
        public async Task<HttpStatusCode> addCarDetails(Car carDetails)
        {
            try
            {
                await database.Vechicles.AddAsync(carDetails);
                await database.SaveChangesAsync();

                return HttpStatusCode.Created;
            }
            catch (SqlException)
            {
               return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<object?> viewCarDetails()
        {
            try
            {
                var carDetails = await database.Vechicles.OfType<Car>().ToListAsync();
                
                if(carDetails != null && carDetails.Count > 0)
                {
                    return carDetails;
                }

                return null;
            }
            catch (SqlException)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}