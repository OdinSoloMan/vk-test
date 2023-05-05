using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UsersGroupConfiguration : IEntityTypeConfiguration<UsersGroup>
    {
        public void Configure(EntityTypeBuilder<UsersGroup> builder)
        {
            builder.Property(u => u.Code)
                .HasConversion<string>();
        }
    }
}
