using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Repository.DbModels
{
    public class RefreshTokens
    {
        [Key]
        public int Id { get; set; }

        public string? Username { get; set; }

        public string RefreshToken { get; set; }


    }
}