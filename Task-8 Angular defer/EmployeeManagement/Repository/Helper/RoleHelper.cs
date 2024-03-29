﻿using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Repository.Database;
using EmployeeManagement.Repository.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace EmployeeManagement.Repository.Helper
{
    public class RoleHelper : IRoleService
    {
        private readonly DatabaseContext database;
        private ApiResponse response;
        
        public RoleHelper(DatabaseContext database)
        {
            this.database = database;
            response = new ApiResponse();
        }

        public async Task<object> GetRole(int id)
        {
            try
            {
                var role = await database.Role.Where(roleId => roleId.Id == id).Select(role => role.Role).FirstOrDefaultAsync();

                return role;

                //response.Status = "Role Created";
                //response.IsError = false;
                //response.HttpStatusCode = (int)HttpStatusCode.Created;
            }
            catch (SqlException)
            {
                //response.Status = "Server Busy";
                //response.IsError = true;
                //response.HttpStatusCode = (int)HttpStatusCode.ServiceUnavailable;

                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                //response.Status = "Something went wrong";
                //response.IsError = true;
                //response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;

                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> NewRole(Roles role)
        {

            try
            {
                await database.Role.AddAsync(role);
                await database.SaveChangesAsync();

                return HttpStatusCode.Created;

                //response.Status = "Role Created";
                //response.IsError = false;
                //response.HttpStatusCode = (int)HttpStatusCode.Created;
            }
            catch (SqlException)
            {
                //response.Status = "Server Busy";
                //response.IsError = true;
                //response.HttpStatusCode = (int)HttpStatusCode.ServiceUnavailable;

                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                //response.Status = "Something went wrong";
                //response.IsError = true;
                //response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;

                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> RemoveRole(int id)
        {
            try
            {
                var roleExit = await database.Role.FindAsync(id);

                if (roleExit != null)
                {
                    database.Role.Remove(roleExit);
                    await database.SaveChangesAsync();

                    return HttpStatusCode.NoContent;

                    //response.Status = "Remove Success";
                    //response.IsError = false;
                    //response.HttpStatusCode = (int)HttpStatusCode.OK;
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
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }

            
        }

        public async Task<HttpStatusCode> UpdateRole(Roles role)
        {

            try
            {
                database.Role.Update(role);
                await database.SaveChangesAsync();

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

        public async Task<object> ViewRoles()
        {

            try
            {
                var roles = await database.Role.Include(employee => employee.EmployeeRoles).ToListAsync();

                if (roles != null && roles.Count > 0)
                {
                    return roles;
                }
                else
                {
                    return HttpStatusCode.NoContent;
                }
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
    }
}
