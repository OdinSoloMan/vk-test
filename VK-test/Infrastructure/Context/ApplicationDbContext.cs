using Infrastructure.Configurations;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Users> User { get; set; }
        public DbSet<UsersState> State { get; set; }
        public DbSet<UsersGroup> Group { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new UsersGroupConfiguration());
            modelBuilder.ApplyConfiguration(new UsersStateConfiguration());
        }
    }
}
