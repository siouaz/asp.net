using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OeuilDeSauron.Data.Items;

namespace OeuilDeSauron.Data.Infrastructure.Configuration
{
    /// <summary>
    /// <see cref="List"/> entity configuration.
    /// </summary>
    public class ListConfiguration : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Editable)
                .HasDefaultValue(true);

            // Relationships
            builder.HasMany(x => x.Items)
                .WithOne(x => x.List)
                .HasForeignKey(x => x.ListId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
