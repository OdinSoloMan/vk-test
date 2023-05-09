using Infrastructure.Configurations;
using Infrastructure.Enums;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext: DbContext
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

            modelBuilder.Entity<UsersState>()
                .HasData(
                    new UsersState { Id = 1, Code = UserStateCode.Active, Description = "Активный пользователей" },
                    new UsersState { Id = 2, Code = UserStateCode.Blocked, Description = "Заблокированый пользователь" }
                );

            modelBuilder.Entity<UsersGroup>()
                .HasData(
                    new UsersGroup { Id = 1, Code = UserGroupCode.Admin, Description = "Aдминистратор" },
                    new UsersGroup { Id = 2, Code = UserGroupCode.User, Description = "Пользователь" }
                );
        }
    }
}
