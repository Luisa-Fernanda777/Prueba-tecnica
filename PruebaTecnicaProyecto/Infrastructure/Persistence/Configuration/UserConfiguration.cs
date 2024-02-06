using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id);

        builder.Property(c => c.OrganizationId);
        builder.Property(c => c.Email).HasMaxLength(50);
        builder.Property(c => c.Password).HasMaxLength(10);
        builder.HasIndex(c => c.Email).IsUnique();

        builder.HasOne(u => u.Organization)
            .WithMany(o => o.Users)
            .HasForeignKey(u => u.OrganizationId);
    }
}