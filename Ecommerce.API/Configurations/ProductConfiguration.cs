using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.API.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name)
               .HasMaxLength(100);

        builder.Property(x => x.Price)
               .HasColumnType("decimal(18,2)");

        // Index on Name
        builder.HasIndex(x => x.Name);

        // Index on Price
        builder.HasIndex(x => x.Price);

        // Composite Index
        builder.HasIndex(x => new
        {
            x.Name,
            x.Price
        });
    }
}