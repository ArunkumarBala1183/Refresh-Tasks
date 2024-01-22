using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JWTAuthentication.Repository.AppModels;
using JWTAuthentication.Repository.DatabaseContext;
using JWTAuthentication.Repository.DbModels;
using JWTAuthentication.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;


public class AuthenticationHelper : IAuthenticationService
{
    private readonly JwtSettings jwtSettings;
    private readonly AuthenticationContext database;
    public AuthenticationHelper(IOptions<JwtSettings> options, AuthenticationContext database)
    {
        this.jwtSettings = options.Value;
        this.database = database;
    }

    private async Task<bool> ValidateUser(UserCredentaials userCredentaials)
    {
        try
        {
            var validUser = await database.UserCredentaials.Where(user => user.Username == userCredentaials.Username && user.Password == userCredentaials.Password).FirstOrDefaultAsync();

            if (validUser != null)
            {
                return true;
            }

            return false;
        }
        catch (SqlException)
        {
            throw;
        }

    }

    private async Task<string> generateToken(UserCredentaials userCredentaials)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var signingKey = Encoding.UTF8.GetBytes(this.jwtSettings.SecurityKey);
        var token = new JwtSecurityToken(
            claims: new Claim[] {
                        new Claim(ClaimTypes.Name , userCredentaials.Username),
                        new Claim(ClaimTypes.Role , userCredentaials.Role)
            },
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
        );
        Log.Information("Before Token Creation");


        var finalToken = tokenHandler.WriteToken(token);

        Log.Information("After Token Generation");

        return finalToken;
    }
    public async Task<object> authenticateUser(UserCredentaials userCredentaials)
    {
        ApiResponse apiResponse = new ApiResponse();
        try
        {
            if (await this.ValidateUser(userCredentaials))
            {
                var token = await this.generateToken(userCredentaials);

                return new TokenResponse()
                {
                    Token = token,
                    RefeshToken = await this.generateRefreshToken(userCredentaials.Username)
                };
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
        }
        catch (SqlException)
        {
            return HttpStatusCode.InternalServerError;
        }
    }

    private async Task<string> generateRefreshToken(string username)
    {
        var randomNumber = new byte[32];

        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            randomNumberGenerator.GetBytes(randomNumber);

            string refreshToken = Convert.ToBase64String(randomNumber);

            var token = await this.database.RefreshTokens.FirstOrDefaultAsync(token => token.Username == username);

            if (token != null)
            {
                token.RefreshToken = refreshToken;
            }
            else
            {
                await this.database.RefreshTokens.AddAsync(new RefreshTokens()
                {
                    RefreshToken = refreshToken,
                    Username = username
                });
            }

            await database.SaveChangesAsync();

            return refreshToken;
        }
    }

    public async Task<object> getRefreshedToken(TokenResponse tokenResponse)
    {
        var existToken = await database.RefreshTokens.Where(token => token.RefreshToken == tokenResponse.RefeshToken).FirstOrDefaultAsync();

        if (existToken != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.UTF8.GetBytes(jwtSettings.SecurityKey);

            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(tokenResponse.Token, new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(securityKey)
            }, out securityToken);

            var token = securityToken as JwtSecurityToken;

            if (token != null && token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                string username = principal.Identity.Name;

                var existUser = await database.RefreshTokens.FirstOrDefaultAsync(user => user.Username == username && user.RefreshToken == tokenResponse.RefeshToken);

                if (existUser != null)
                {
                    var newToken = new JwtSecurityToken(
                        claims: principal.Claims.ToArray(),
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256)
                    );

                    return new TokenResponse()
                    {
                        Token = tokenHandler.WriteToken(newToken),
                        RefeshToken = await this.generateRefreshToken(username)
                    };
                }
                else
                {
                    return HttpStatusCode.Unauthorized;
                }
            }
            else
            {
                return HttpStatusCode.Unauthorized;
            }
        }
        else
        {
            return HttpStatusCode.Unauthorized;
        }

    }
}