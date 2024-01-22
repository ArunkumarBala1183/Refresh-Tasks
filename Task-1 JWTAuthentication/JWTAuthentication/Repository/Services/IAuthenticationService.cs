using System.Net;
using JWTAuthentication.Repository.AppModels;
using JWTAuthentication.Repository.DbModels;

namespace JWTAuthentication.Repository.Services
{
    public interface IAuthenticationService
    {
        public Task<object> authenticateUser(UserCredentaials userCredentaials);

        public Task<object> getRefreshedToken(TokenResponse tokenResponse);
    }
}