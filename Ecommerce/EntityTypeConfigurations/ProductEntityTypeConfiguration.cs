using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EntityTypeConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.DescriptionShort)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(19,4)");
        
        // builder.Property(x => x.Description)
        //     .IsRequired();
        //
        // builder.Property(x => x.Ean)
        //     .HasMaxLength(13);
        //
        // builder.Property(x => x.Sku)
        //     .HasMaxLength(50);
        //
        // builder.Property(x => x.Mpn)
        //     .HasMaxLength(50);
        //
        // builder.Property(x => x.PackageWeight)
        //     .HasColumnType("decimal(18,2)");
        //
        // builder.Property(x => x.PackageWidth)
        //     .HasColumnType("decimal(18,2)");
        //
        // builder.Property(x => x.PackageHeight)
        //     .HasColumnType("decimal(18,2)");
        //
        // builder.Property(x => x.PackageLength)
        //     .HasColumnType("decimal(18,2)");
        //
        // builder.Property(x => x.TaxGroupId)
        //     .IsRequired();
        //
        // builder.Property(x => x.ProductCategoryId)
        //     .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}