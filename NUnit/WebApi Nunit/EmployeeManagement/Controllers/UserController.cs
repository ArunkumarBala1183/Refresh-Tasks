using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService user;
        public UserController(IUserService user)
        {
            this.user = user;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserCredentials credentials)
        {
            var response = await user.AddUser(credentials);

            if (response.Equals(HttpStatusCode.Created))
            {
                return Created();
            }
            else
            {
                return StatusCode((int)response);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await user.DeleteUser(id);

            if (response.Equals(HttpStatusCode.NoContent))
            {
                return NoContent();
            }
            else
            {
                return StatusCode((int)response);
            }
        }
    }
}
