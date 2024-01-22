using JWTAuthentication.Repository.DbModels;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthentication.Repository.DatabaseContext
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        { }

        public DbSet<UserCredentaials> UserCredentaials { get; set; }

        public DbSet<RefreshTokens> RefreshTokens { get; set; }
    }
}