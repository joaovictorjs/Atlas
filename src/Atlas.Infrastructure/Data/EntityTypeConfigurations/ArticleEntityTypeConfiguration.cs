using Atlas.Domain.Entities;
using Atlas.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Data.EntityTypeConfigurations
{
    internal class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("articles").HasKey(article => article.Id);
            builder.Property(article => article.Id).HasColumnName("id");
            builder.Property(article => article.Title).HasColumnName("title").IsRequired();
            builder.Property(article => article.Slug).HasColumnName("slug").IsRequired();
            builder.Property(article => article.Content).HasColumnName("content").IsRequired();
            builder
                .Property(article => article.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion(
                    statusEnum => statusEnum.ToString(),
                    str => Enum.Parse<ArticleStatus>(str)
                );
            builder
                .Property(article => article.PublishedAt)
                .HasColumnName("published_at")
                .IsRequired();
            builder.Property(article => article.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(article => article.UpdatedAt).HasColumnName("updated_at").IsRequired();
        }
    }
}
