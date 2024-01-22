using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Repository.DbModels
{
    public class UserCredentaials
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}