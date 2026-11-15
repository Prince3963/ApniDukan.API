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
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(od => od.discount)
                .HasPrecision(18, 3)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(od => od.BillAmount)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.HasOne(x => x.Order)
           .WithMany(x => x.OrderDetails)
           .HasForeignKey(x => x.OrderId)
           .OnDelete(DeleteBehavior.Cascade);

            // Product (1) -> OrderDetails (Many)
            builder.HasOne(x => x.Products)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.OrderId);

            builder.HasIndex(x => x.ProductId);
        }
    }
}
