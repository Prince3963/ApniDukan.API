using ApniDukan.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApniDukan.Infrastrcture.Data.Configurations
{
    public static class GenericEntityConfiguration
    {
        public static void ConfigureGenericEntity<T>(
        this EntityTypeBuilder<T> builder)
        where T : GenericEntity
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .IsRequired(false);

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(e => e.DeletedAt)
                .IsRequired(false);

            // Soft delete global query filter
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
