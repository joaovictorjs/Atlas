using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Atlas.Domain.Enums;
using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public partial class Article : BaseEntity
{
    public string Title { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public string Content { get; private set; } = string.Empty;

    public ArticleStatus Status { get; private set; }

    public DateTime? PublishedAt { get; private set; }

    public Article(string title, string content)
    {
        ValidateTitle(title);
        ValidateContent(content);

        Title = title;
        Slug = GenerateSlug(title);
        Content = content;
        Status = ArticleStatus.Draft;
    }

    public void ChangeTitle(string title)
    {
        ValidateTitle(title);
        Title = title;
        Slug = GenerateSlug(title);
        MarkAsUpdated();
    }

    public void ChangeContent(string content)
    {
        ValidateContent(content);
        Content = content;
        MarkAsUpdated();
    }

    public void Publish()
    {
        if (Status == ArticleStatus.Published)
        {
            return;
        }

        Status = ArticleStatus.Published;
        PublishedAt = DateTime.UtcNow;
        MarkAsUpdated();
    }

    public void RevertToDraft()
    {
        if (Status == ArticleStatus.Draft)
        {
            return;
        }

        Status = ArticleStatus.Draft;
        PublishedAt = null;
        MarkAsUpdated();
    }

    public void Archive()
    {
        if (Status == ArticleStatus.Archived)
        {
            return;
        }

        Status = ArticleStatus.Archived;
        PublishedAt = null;
        MarkAsUpdated();
    }

    private Article() { }

    private static string GenerateSlug(string title)
    {
        var slug = title
            .ToLowerInvariant()
            .Trim()
            .Replace(' ', '-')
            .Normalize(NormalizationForm.FormD);
        var strBuilder = new StringBuilder();

        foreach (var c in slug)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                strBuilder.Append(c);
            }
        }

        slug = strBuilder.ToString().Normalize(NormalizationForm.FormC);
        slug = DashedAlphanumericRegex().Replace(slug, string.Empty);
        slug = MultiDashRegex().Replace(slug, "-");
        slug = slug.Trim('-');
        return slug;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                ExceptionMessages.TitleCantBeNullOrWhiteSpace,
                nameof(title)
            );
        }
    }

    private static void ValidateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException(
                ExceptionMessages.ContentCantBeNullOrWhiteSpace,
                nameof(content)
            );
        }
    }

    [GeneratedRegex("[^a-z0-9-]")]
    private static partial Regex DashedAlphanumericRegex();

    [GeneratedRegex("-{2,}")]
    private static partial Regex MultiDashRegex();
}
