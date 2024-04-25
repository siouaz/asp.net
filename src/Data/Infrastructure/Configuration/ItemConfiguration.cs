using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using siwar.Data.Items;

namespace siwar.Data.Infrastructure.Configuration
{
    /// <summary>
    /// <see cref="Item"/> entity configuration.
    /// </summary>
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.Code)
                .HasMaxLength(128);

            builder.Ignore(x => x.Extra);

            // Indexes
            builder.HasIndex(x => x.Name);
        }
    }
}
