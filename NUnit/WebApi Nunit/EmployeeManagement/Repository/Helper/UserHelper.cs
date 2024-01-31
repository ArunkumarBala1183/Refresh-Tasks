using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Database;
using EmployeeManagement.Repository.Services;
using Microsoft.Data.SqlClient;
using System.Net;

namespace EmployeeManagement.Repository.Helper
{
    public class UserHelper : IUserService
    {
        private readonly DatabaseContext database;
        public UserHelper(DatabaseContext database)
        {
            this.database = database;
        }
        public async Task<HttpStatusCode> AddUser(UserCredentials credentials)
        {
            try
            {
                await this.database.UserCredentials.AddAsync(credentials);
                await this.database.SaveChangesAsync();

                return HttpStatusCode.Created;
            }
            catch (SqlException)
            {
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> DeleteUser(int id)
        {
            try
            {
                var userExist = await database.UserCredentials.FindAsync(id);

                if(userExist != null) 
                {
                    database.UserCredentials.Remove(userExist);
                    await database.SaveChangesAsync();

                    return HttpStatusCode.NoContent;
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }
            }
            catch (SqlException)
            {
                return HttpStatusCode.ServiceUnavailable;
            }
            catch(Exception)
            {
                return HttpStatusCode.InternalServerError;
            }

        }
    }
}
