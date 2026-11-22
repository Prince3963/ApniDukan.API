using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApniDukan.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApniDukan.Infrastrcture.Data.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shops");

            builder.ConfigureGenericEntity();

            builder.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Description)
                .HasMaxLength(1000) 
                .IsRequired();

            builder.Property(s => s.Address)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(s => s.IsOpen)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(s => s.ImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            //ShopOwner(One) - Shops(Many)
            builder.HasOne(s => s.ShopOwner)
                .WithMany(u => u.Shops)
                .HasForeignKey(s => s.ShopOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => s.ShopOwnerId);

            builder.HasIndex(s => s.IsActive);

            builder.HasIndex(s => s.IsOpen);
        }
    }
}
