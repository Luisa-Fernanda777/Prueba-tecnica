using Domain.Organizations;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>{
    public void Configure(EntityTypeBuilder<Product> builder){
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(50);
        builder.Property(c => c.Price).HasMaxLength(50);

        builder.HasOne(o => o.Organization)
                .WithMany(u => u.Products)
                .HasForeignKey(u => u.OrganizationId);
    }
}