using Atlas.Exceptions;
using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities
{
    public class ArticleTag : BaseEntity
    {
        public int ArticleId { get; private set; }
        public int TagId { get; private set; }

        // Navigation
        public Article Article { get; private set; } = null!;
        public Tag Tag { get; private set; } = null!;

        public ArticleTag(Article article, Tag tag)
        {
            ValidateArticle(article);
            ValidateTag(tag);

            ArticleId = article.Id;
            TagId = tag.Id;
            Article = article;
            Tag = tag;
        }

        private ArticleTag() { }

        private static void ValidateArticle(Article article)
        {
            DomainException.ThrowIfNull(article, ExceptionMessages.ArticleCantBeNull);
        }

        private static void ValidateTag(Tag tag)
        {
            DomainException.ThrowIfNull(tag, ExceptionMessages.TagCantBeNull);
        }
    }
}
