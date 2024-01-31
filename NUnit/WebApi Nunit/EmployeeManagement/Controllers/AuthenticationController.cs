using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Handler;
using EmployeeManagement.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService authentication;
        public AuthenticationController(IAuthenticateService authentication)
        {
            this.authentication = authentication;
        }

        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(UserCredentialsDto userCredentials)
        {
            try
            {
                var response = await authentication.AuthenticateCredentials(userCredentials);

                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (CustomException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost("GenerateNewToken")]
        public async Task<IActionResult> GenerateNewToken (Token token)
        {
            var response = await this.authentication.GenerateNewToken(token);
            
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
