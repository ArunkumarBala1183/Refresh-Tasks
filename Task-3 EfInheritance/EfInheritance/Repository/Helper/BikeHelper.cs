using System.Net;
using EfInheritance.Repository.DatabaseContext;
using EfInheritance.Repository.DbModels;
using EfInheritance.Repository.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EfInheritance.Repository.Helper
{
    public class BikeHelper : IBikeServices
    {
        private readonly InheritanceContext database;
        public BikeHelper(InheritanceContext context)
        {
            this.database = context;
        }
        public async Task<HttpStatusCode> addBikeDetails(Bike bikeDetails)
        {
            try
            {
                await database.Vechicles.AddAsync(bikeDetails);
                await database.SaveChangesAsync();

                return HttpStatusCode.Created;
            }
            catch (SqlException)
            {
               return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<object?> viewBikeDetails()
        {
            try
            {
                var carDetails = await database.Vechicles.OfType<Bike>().ToListAsync();
                
                if(carDetails != null && carDetails.Count > 0)
                {
                    return carDetails;
                }

                return null;
            }
            catch (SqlException error)
            {
                Log.Information(error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}