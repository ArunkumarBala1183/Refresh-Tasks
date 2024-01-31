using AutoMapper;
using JWTAuthentication.Repository.DbModels;
using JWTAuthentication.Repository.DTO;

namespace JWTAuthentication.Repository.Services
{
    class AutomapperHelper : Profile
    {
        public AutomapperHelper()
        {
            CreateMap<UserCredentialsDTO , UserCredentaials>().ReverseMap();
        }
    }
}