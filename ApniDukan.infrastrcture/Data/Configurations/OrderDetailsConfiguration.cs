using ApniDukan.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApniDukan.Infrastrcture.Data.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");

            builder.ConfigureGenericEntity();

            builder.Property(od => od.Quantity)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(od => od.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(od => od.Discount)
                .HasPrecision(18, 2)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(od => od.BillAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            // Order (One) -> OrderDetails (Many)
            builder.HasOne(x => x.Orders)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product (One) -> OrderDetails (Many)
            builder.HasOne(x => x.Products)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.OrderId);

            builder.HasIndex(x => x.ProductId);
        }
    }
}
