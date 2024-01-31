using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Database;
using EmployeeManagement.Repository.Handler;
using EmployeeManagement.Repository.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagement.Repository.Helper
{
    public class AuthenticationHelper : IAuthenticateService
    {
        private readonly DatabaseContext database;
        private readonly JwtSettings jwtOptions;
        private readonly IEmailService service;
        public AuthenticationHelper(DatabaseContext database, IOptions<JwtSettings> jwtOptions , IEmailService emailService)
        {
            this.database = database;
            this.jwtOptions = jwtOptions.Value;
            this.service = emailService;
        }
        public async Task<object> AuthenticateCredentials(UserCredentialsDto credentials , bool isOtpVerified)
        {
            try
            {
                var userExists = await database.UserCredentials.FirstOrDefaultAsync(user =>
                user.UserName == credentials.UserName && user.Password == credentials.Password);

                if (userExists != null)
                {
                    if (isOtpVerified)
                    {
                        var token = await this.GenerateToken(userExists);

                        return token;
                    }
                    else
                    {
                        var otp = this.generateOtp();
                        await this.multifactorAuthenticate(userExists.EmployeeId , otp);

                        return new OTPResponse()
                        {
                            OTP = otp,
                            OTPStatus = "Please Verify OTP"
                        };
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException)
            {
                throw new CustomException("Server Unreachable");
            }
            catch (Exception)
            {
                throw new CustomException("Something went wrong");
            }
        }

        public async Task<object> GenerateNewToken(Token token)
        {
            var validUser = await database.RefreshTokens.FirstOrDefaultAsync(token => token.RefreshToken == token.RefreshToken);

            if (validUser != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var securityKey = Encoding.UTF8.GetBytes(jwtOptions.SecurityKey);

                SecurityToken securityToken;

                var principal = tokenHandler.ValidateToken(
                    token.CurrentToken,
                    new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                        ValidateIssuerSigningKey = true
                    },
                    out securityToken);

                var validatedToken = securityToken as JwtSecurityToken;

                if (validatedToken != null && validatedToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    string username = principal.Identity.Name;

                    var exitUser = await database.RefreshTokens.FirstOrDefaultAsync(user => user.UserName == username && user.RefreshToken == token.RefreshToken);

                    if (exitUser != null)
                    {
                        var newToken = new JwtSecurityToken(
                            claims: principal.Claims.ToArray(),
                            expires: DateTime.UtcNow.AddMinutes(1),
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256)
                            );
                        var finalToken = tokenHandler.WriteToken(newToken);


                        return new Token()
                        {
                            CurrentToken = finalToken,
                            RefreshToken = await this.GenerateRefreshToken(username)
                        };
                    }
                }
            }

            return null;
        }
        private async Task<string> GenerateRefreshToken(string username)
        {
            var randomNumber = new byte[32];

            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);

                var refreshToken = Convert.ToBase64String(randomNumber);

                var exitUser = await database.RefreshTokens.FirstOrDefaultAsync(user => user.UserName == username);

                if (exitUser != null)
                {
                    exitUser.RefreshToken = refreshToken;
                }
                else
                {
                    await this.database.RefreshTokens.AddAsync(new RefreshTokens()
                    {
                        UserName = username,
                        RefreshToken = refreshToken
                    });
                }

                await database.SaveChangesAsync();

                return refreshToken;
            }

        }

        private async Task<Token> GenerateToken(UserCredentials credentials)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(jwtOptions.SecurityKey);

            var token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name , credentials.UserName),
                    new Claim(ClaimTypes.Role , credentials.Role)
                },
                expires: DateTime.UtcNow.AddSeconds(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
                );

            var newToken = tokenHandler.WriteToken(token);

            return new Token()
            {
                CurrentToken = newToken,
                RefreshToken = await this.GenerateRefreshToken(credentials.UserName)
            };

        }

        private int generateOtp()
        {
            Random randomNumber = new Random();

            return randomNumber.Next(1000,9999);
        }

        private async Task multifactorAuthenticate(int employeeId , int otp)
        {
            string email = await database.Employees.Where(user => user.EmployeeId == employeeId).Select(id => id.EmailId).FirstOrDefaultAsync();

            EmailRequest emailRequest = new EmailRequest()
            {
                ToEmailAddress = email,
                Subject = "Login Request",
                Body = $"Otp for Login : {otp}"
            };

            service.SendEmail(emailRequest);
        }

    }
}
