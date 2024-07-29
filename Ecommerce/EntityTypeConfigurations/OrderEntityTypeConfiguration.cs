using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EntityTypeConfigurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CustomerId)
                .IsRequired();

            // builder.Property(x => x.Email)
            //     .IsRequired()
            //     .HasMaxLength(254);
            //
            // builder.Property(x => x.ItemsNet)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.ItemsTax)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.ItemsGross)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.ShippingCost)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.ShippingOptionId)
            //     .IsRequired();
            //
            // builder.Property(x => x.ShippingOptionName)
            //     .IsRequired()
            //     .HasMaxLength(100);
            //
            // builder.Property(x => x.ShippingOptionDriver)
            //     .IsRequired()
            //     .HasMaxLength(100);
            //
            // builder.Property(x => x.TotalNet)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.TotalTax)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.TotalGross)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.TotalWeight)
            //     .IsRequired()
            //     .HasColumnType("decimal(18,2)");
            //
            // builder.Property(x => x.HasCustomerNote)
            //     .IsRequired();
            //
            // builder.Property(x => x.CustomerNote)
            //     .HasMaxLength(255);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();
        }
    }