using AutoMapper;
using Azure;
using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Database;
using EmployeeManagement.Repository.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;
using System.Data;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagement.Repository.Helper
{
    public class EmployeeHelper : IEmployeeService
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        private readonly EmailConfig emailConfig;
        private readonly IEmailService emailService;
        private readonly IWebHostEnvironment environment;
        public EmployeeHelper(DatabaseContext database, IMapper mapper , IOptions<EmailConfig> emailConfig, IWebHostEnvironment environment , IEmailService emailService)
        {
            this.database = database;
            this.mapper = mapper;
            this.emailConfig = emailConfig.Value;
            this.environment = environment;
            this.emailService = emailService;
        }
        public async Task<HttpStatusCode> DeleteEmployee(int id)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeData = await database.Employees
                    .Include(address => address.Address)
                    .Where(item => item.EmployeeId == id)
                    .FirstOrDefaultAsync();

                if (employeeData == null)
                {
                    return HttpStatusCode.NoContent;
                }
                else
                {
                    database.Employees.Remove(employeeData);
                    await database.SaveChangesAsync();

                    return HttpStatusCode.OK;
                }

            }
            catch (DbUpdateException)
            {
                return HttpStatusCode.AlreadyReported;
            }
            catch (SqlException)
            {
               return HttpStatusCode.InternalServerError;
            }
            catch (Exception)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        private Employee DtoMapper(EmployeeDto employeeDetails)
        {
            Employee employee = new Employee();

            employee = mapper.Map<EmployeeDto, Employee>(employeeDetails);

            return employee;
        }
        private List<EmployeeDto> MapToDto(List<Employee> employees)
        {

            List<EmployeeDto> employeeDto = new List<EmployeeDto>();

            employeeDto = mapper.Map<List<EmployeeDto>>(employees);

            return employeeDto;

        }

        private async Task SendEmail(UserCredentials userCredentials , string toMail)
        {
            try
            {
                string body = string.Empty;
                string filePath = environment.ContentRootPath + "//Repository//EmailBody//AccountCreated.html";

                if (File.Exists(filePath))
                {
                    body = File.ReadAllText(filePath);

                    string? name = await database.Employees.Where(id => id.EmployeeId == userCredentials.EmployeeId).Select(id => id.Name).FirstOrDefaultAsync();
                    
                    body = body.Replace("[name]", name).
                    Replace("[username]", userCredentials.UserName).
                    Replace("[password]", userCredentials.Password);
                }
                
                EmailRequest request = new EmailRequest()
                {
                    ToEmailAddress = toMail,
                    Subject = "Account Created",
                    Body = body
                };

                await emailService.SendEmail(request);

            }
            catch (Exception error)
            {
                Log.Error(error.Message);
            }
        }

        private async Task GenerateUserCredentials(string emailId)
        {
            try
            {
                var userDetails = await database.Employees.Where(user => user.EmailId == emailId).FirstOrDefaultAsync();

                if (userDetails != null)
                {

                    UserCredentials userCredentials = new UserCredentials()
                    {
                        UserName = userDetails.Name + $"{userDetails.DateofBirth.Day :D2}" + $"{userDetails.DateofBirth.Month :D2}",
                        Password = Guid.NewGuid().ToString(),
                        EmployeeId = userDetails.EmployeeId,
                    };

                    await database.UserCredentials.AddAsync(userCredentials);
                    await database.SaveChangesAsync();

                    //await SendEmail(userCredentials, userDetails.EmailId);


                }
            }
            catch (Exception error)
            {
                Log.Error(error.Message);
            }
        }

        public async Task<ApiResponse> NewEmployee(EmployeeDto employeeDetails)
        {
            var employee = this.DtoMapper(employeeDetails);
            ApiResponse response = new ApiResponse();
            try
            {
                await database.Employees.AddAsync(employee);
                await database.SaveChangesAsync();

                await this.GenerateUserCredentials(employee.EmailId);

                response.Status = "Record Added Successfully";
                response.IsError = false;
                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (DbUpdateException error)
            {
                response.Status = error.Message;
                response.IsError = true;
                response.HttpStatusCode = (int)HttpStatusCode.AlreadyReported;
            }
            catch (SqlException)
            {
                response.Status = "Server Busy";
                response.IsError = true;
                response.HttpStatusCode = (int)HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                response.Status = "Something went wrong";
                response.IsError = true;
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<HttpStatusCode> UpdateEmployee(EmployeeDto employeeDetails)
        {
            Employee employee = this.DtoMapper(employeeDetails);
            ApiResponse response = new ApiResponse();
            try
            {
                database.Employees.Update(employee);
                await database.SaveChangesAsync();

                return HttpStatusCode.OK;
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

        public async Task<object> GetEmployee(int id)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeDetail = await database.Employees.Include(address => address.Address)
                    .Include(role => role.EmployeeRoles).FirstOrDefaultAsync(employeeId => employeeId.EmployeeId == id);
            
                if(employeeDetail != null)
                {
                    var employeeDtos = mapper.Map<EmployeeDto>(employeeDetail);

                    return employeeDtos;
                }

                return HttpStatusCode.NoContent;
            }
            catch (SqlException)
            {
                // response.Status = "Server Busy";
                // response.IsError = true;
                // response.HttpStatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                // Console.WriteLine(error.Message);
                // response.Status = "Something went wrong";
                // response.IsError = true;
                // response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<object> ViewEmployees()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employee = await this.database.Employees.
                    Include(address => address.Address)
                    .Include(role => role.EmployeeRoles)
                    .ToListAsync();

                var employeeDtos = this.MapToDto(employee);

                return employeeDtos;
            }
            catch (SqlException)
            {
                response.Status = "Server Busy";
                response.IsError = true;
                response.HttpStatusCode = (int)HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                response.Status = "Something went wrong";
                response.IsError = true;
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<ApiResponse> RoleUpdate(int id, List<EmployeeRoleDto> roles)
        {

            ApiResponse response = new ApiResponse();
            try
            {
                var userExist = await database.Employees.Include(role => role.EmployeeRoles).FirstOrDefaultAsync(employee => employee.EmployeeId == id);
                
                if (userExist != null)
                {
                    var employeeRoles = userExist.EmployeeRoles.ToList();

                    if(employeeRoles.Count > 0) 
                    {

                        roles.ForEach(role =>
                        {
                            var exitRole = employeeRoles.FirstOrDefault(existId => existId.RoleId == role.RoleId);

                            if(exitRole == null)
                            {
                                EmployeeRole employeeRole = new EmployeeRole()
                                {
                                    EmployeeId = id,
                                    RoleId = role.RoleId
                                };

                                employeeRoles.Add(employeeRole);
                            }
                        });

                        userExist.EmployeeRoles = employeeRoles;

                        await database.SaveChangesAsync();

                        response.Status = "Role Updated";
                        response.IsError = false;
                        response.HttpStatusCode = (int)HttpStatusCode.OK;
                    }
                    else
                    {
                        roles.ForEach(newRole =>
                        {
                            EmployeeRole employeeRole = new EmployeeRole()
                            {
                                EmployeeId = id,
                                RoleId = newRole.RoleId
                            };

                            userExist.EmployeeRoles.Add(employeeRole);
                        });

                        await database.SaveChangesAsync();

                        response.Status = "Role Updated";
                        response.IsError = false;
                        response.HttpStatusCode = (int)HttpStatusCode.OK;
                    }
                }
                else
                {
                    response.Status = "Employee Not Found";
                    response.IsError = false;
                    response.HttpStatusCode = (int)HttpStatusCode.NotFound;
                }
            }
            catch (SqlException)
            {
                response.Status = "Server Busy";
                response.IsError = true;
                response.HttpStatusCode = (int)HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                response.Status = "Something went wrong";
                response.IsError = true;
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
