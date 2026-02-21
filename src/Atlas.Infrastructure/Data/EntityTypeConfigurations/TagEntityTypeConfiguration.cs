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

            builder
                .Property(tag => tag.Name)
                .IsRequired()
                .HasMaxLength(Tag.NameMaxLength)
                .HasColumnName("name");

            builder.Property(tag => tag.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(tag => tag.UpdatedAt).IsRequired().HasColumnName("updated_at");
        }
    }
}
