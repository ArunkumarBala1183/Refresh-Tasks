using AutoMapper;
using JWTAuthentication.Repository.DbModels;
using JWTAuthentication.Repository.DTOs;

namespace JWTAuthentication.Repository.Helper
{
    public class AutomapperHelper : Profile
    {
        public AutomapperHelper()
        {
            CreateMap<UserCredentaials , UserCrendentialsDTO>().ReverseMap();
        }   
    }
}