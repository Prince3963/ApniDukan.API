using ApniDukan.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApniDukan.Infrastrcture.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.ConfigureGenericEntity();

            builder.Property(o => o.TotalAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired();

            //Relationship (One To Manry)
            builder.HasOne(o => o.Customers)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Shop (One) -> Orders (Many)
            builder.HasOne(o => o.Shops)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order (One) -> OrderDetails (Many)
            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Orders)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.UserID);

            builder.HasIndex(o => o.ShopId);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
