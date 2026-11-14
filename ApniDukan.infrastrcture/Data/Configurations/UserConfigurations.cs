using ApniDukan.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApniDukan.Infrastrcture.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.ConfigureGenericEntity();

            builder.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Password)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(u => u.AlternativeNumber)
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(u => u.RoomMatesNumber)
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(u => u.RoomMatesAlternativeNumber)
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(u => u.RoomOwnerNumber)
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(x => x.CurrentAddress)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.PermanentAddress)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.ProfileImage)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(u => u.DocumentImage)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            //Relationship (one to many)
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.Users)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
