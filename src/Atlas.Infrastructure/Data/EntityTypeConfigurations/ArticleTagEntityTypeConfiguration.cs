using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Data.EntityTypeConfigurations
{
    internal class ArticleTagEntityTypeConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.ToTable("articles_tags").HasKey(articleTag => articleTag.Id);
            builder.Property(articleTag => articleTag.Id).HasColumnName("id");
            builder.Property(articleTag => articleTag.ArticleId).HasColumnName("article_id");
            builder.Property(articleTag => articleTag.TagId).HasColumnName("tag_id");
            builder.Property(articleTag => articleTag.CreatedAt).HasColumnName("created_at");
            builder.Property(articleTag => articleTag.UpdatedAt).HasColumnName("updated_at");
            builder.HasIndex(at => new { at.ArticleId, at.TagId }).IsUnique();

            builder
                .HasOne(articleTag => articleTag.Article)
                .WithMany(article => article.ArticleTags)
                .HasForeignKey(articleTag => articleTag.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(articleTag => articleTag.Tag)
                .WithMany(tag => tag.ArticleTags)
                .HasForeignKey(articleTag => articleTag.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
