using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UsersStateConfiguration : IEntityTypeConfiguration<UsersState>
    {
        public void Configure(EntityTypeBuilder<UsersState> builder)
        {
            builder.Property(u => u.Code)
                .HasConversion<string>();
        }
    }
}
