using EmployeeManagement.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
        
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Address> Address { get; set; }
        
        public DbSet<Roles> Role { get; set; }

        public DbSet<UserCredentials> UserCredentials { get; set; }

        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .HasMany(roles => roles.EmployeeRoles)
                .WithOne(employee => employee.Employee)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Roles>()
                .HasMany(employee => employee.EmployeeRoles)
                .WithOne(role => role.Role)
                .OnDelete(DeleteBehavior.Cascade);
        }




    }
}
