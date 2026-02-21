using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Data.EntityTypeConfigurations
{
    internal class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tags");
            builder.HasKey(tag => tag.Id);
            builder.Property(tag => tag.Id).HasColumnName("id");
            builder.Property(tag => tag.CreatorId).HasColumnName("creator_id");

            builder
                .Property(tag => tag.Name)
                .IsRequired()
                .HasMaxLength(Tag.NameMaxLength)
                .HasColumnName("name");

            builder.HasIndex(tag => tag.Name).IsUnique();
            builder.Property(tag => tag.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(tag => tag.UpdatedAt).IsRequired().HasColumnName("updated_at");

            builder
                .HasOne(tag => tag.Creator)
                .WithMany(user => user.CreatedTags)
                .HasForeignKey(tag => tag.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
