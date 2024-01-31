using System.Net;
using JWTAuthentication.Repository.AppModels;
using JWTAuthentication.Repository.DbModels;
using JWTAuthentication.Repository.DTOs;

namespace JWTAuthentication.Repository.Services
{
    public interface IAuthenticationService
    {
        public Task<object> authenticateUser(UserCrendentialsDTO userCredentaials);

        public Task<object> getRefreshedToken(TokenResponse tokenResponse);
    }
}