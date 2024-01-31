using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Handler;
using EmployeeManagement.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService authentication;
        private readonly OTPResponse otpRepository;
        public AuthenticationController(IAuthenticateService authentication, OTPResponse otpRepository)
        {
            this.authentication = authentication;
            this.otpRepository = otpRepository;
        }

        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserCredentialsDto userCredentials)
        {
            try
            {
                var response = await authentication.AuthenticateCredentials(userCredentials, otpRepository.isOtpVerified);

                if (response != null && response.GetType() == typeof(OTPResponse))
                {
                    OTPResponse otpResponse = (OTPResponse)response;
                    otpRepository.OTP = otpResponse.OTP;

                    return Ok(new { status = otpResponse.OTPStatus });
                }
                else if (response != null)
                {
                    if (otpRepository.isOtpVerified)
                    {
                        otpRepository.Dispose(true);
                    }
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

        [HttpPost("OTPValidate")]
        public async Task<IActionResult> OTPValidate([FromForm]string OTP)
        {
            try
            {
                string storedOTP = Convert.ToString(otpRepository.OTP);

                if (storedOTP != null)
                {
                    if (string.Equals(storedOTP, OTP))
                    {
                        otpRepository.isOtpVerified = true;
                        return Ok(new { status = "OTP Verified" });
                    }

                    return Unauthorized();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception error)
            {
                Log.Information("Error : " + error.Message);
                return Unauthorized();
            }
        }

        [HttpPost("GenerateNewToken")]
        public async Task<IActionResult> GenerateNewToken(Token token)
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
