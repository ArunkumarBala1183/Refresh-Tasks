using EfInheritance.Repository.DbModels;
using Microsoft.EntityFrameworkCore;

namespace EfInheritance.Repository.DatabaseContext
{
    public class InheritanceContext : DbContext
    {
        public InheritanceContext(DbContextOptions<InheritanceContext> options) : base(options)
        {
        }

        public DbSet<Vechicle> Vechicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vechicle>().UseTphMappingStrategy();
            modelBuilder.Entity<Car>();
            modelBuilder.Entity<Bike>();
            base.OnModelCreating(modelBuilder);
        }
    }
}