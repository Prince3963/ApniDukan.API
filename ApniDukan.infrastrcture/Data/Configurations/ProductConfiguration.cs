using ApniDukan.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApniDukan.Infrastrcture.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.ConfigureGenericEntity();

            builder.Property(p => p.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(p => p.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .HasPrecision(18, 3)
                .IsRequired();

            builder.Property(p => p.Unit)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            //Relationship One To Many
            builder.HasOne(p => p.Categories)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.OrderDetails)
                .WithOne(od => od.Products)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.IsActive);
        }
    }
}
