using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Data.EntityTypeConfigurations
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");
            builder.HasKey(category => category.Id);
            builder.Property(category => category.Id).HasColumnName("id");
            builder.Property(category => category.CreatorId).HasColumnName("creator_id");

            builder
                .Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(Category.NameMaxLength)
                .HasColumnName("name");

            builder
                .Property(category => category.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(category => category.UpdatedAt)
                .IsRequired()
                .HasColumnName("updated_at");

            builder.HasIndex(category => category.Name).IsUnique();

            builder
                .HasOne(category => category.Creator)
                .WithMany(user => user.CreatedCategories)
                .HasForeignKey(category => category.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
