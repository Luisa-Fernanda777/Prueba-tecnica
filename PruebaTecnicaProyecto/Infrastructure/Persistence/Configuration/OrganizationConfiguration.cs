using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.Persistence.Configuration;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>{
    public void Configure(EntityTypeBuilder<Organization> builder){

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(50);
        builder.Property(c => c.SlugTenant).HasMaxLength(50);

        builder.HasMany(o => o.Users)
                .WithOne(u => u.Organization)
                .HasForeignKey(u => u.OrganizationId);
        
        builder.HasMany(o => o.Products)
                .WithOne(u => u.Organization)
                .HasForeignKey(u => u.OrganizationId);
    }
}