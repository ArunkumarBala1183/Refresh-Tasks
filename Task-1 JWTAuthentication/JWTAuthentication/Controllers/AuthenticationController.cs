using System.Net;
using JWTAuthentication.Repository.AppModels;
using JWTAuthentication.Repository.DbModels;
using JWTAuthentication.Repository.DTOs;
using JWTAuthentication.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace JWTAuthentication.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AuthenticationController:ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        
        [HttpPost("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails([FromBody] UserCrendentialsDTO usercredentials)
        {
            var response = await authenticationService.authenticateUser(usercredentials);
            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("RegenerateToken")]
        public async Task<IActionResult> RegenerateToken([FromBody] TokenResponse tokenResponse)
        {
            var response = await authenticationService.getRefreshedToken(tokenResponse);
            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
    }
}