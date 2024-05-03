using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OeuilDeSauron.Data.Items;

namespace OeuilDeSauron.Data.Infrastructure.Configuration
{
    public class ItemRelationConfiguration : IEntityTypeConfiguration<ItemRelation>
    {

        public void Configure(EntityTypeBuilder<ItemRelation> builder)
        {
            // Key
            builder.HasKey(x => new { x.ParentId, x.ChildId });

            // Relationships
            builder.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Child)
                   .WithMany(x => x.Parents)
                   .HasForeignKey(x => x.ChildId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
