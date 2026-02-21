using Atlas.Domain.Enums;
using Atlas.Exceptions.Resources;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Atlas.Domain.Entities;

public partial class Article : BaseEntity
{
    public const int TitleMinLength = 4;
    public const int TitleMaxLength = 255;

    public int CreatorId { get; private set; }
    public int? PublisherId { get; private set; }  
    public string Title { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public ArticleStatus Status { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    // Navigation
    public User Creator { get; private set; } = null!;
    public User? Publisher { get; private set; }

    public Article(string title, string content, User creator)
    {
        ValidateTitle(title);
        ValidateContent(content);
        ValidateCreator(creator);

        Creator = creator;
        CreatorId = Creator.Id;
        Title = title;
        Slug = GenerateSlug(title);
        Content = content;
        Status = ArticleStatus.Draft;
    }

    private Article() { }

    public void ChangeTitle(string title)
    {
        ValidateTitle(title);

        RevertToDraft();
        Title = title;
        Slug = GenerateSlug(title);
        MarkAsUpdated();
    }

    public void ChangeContent(string content)
    {
        ValidateContent(content);

        RevertToDraft();
        Content = content;
        MarkAsUpdated();
    }

    public void Publish(User publisher)
    {
        if (Status == ArticleStatus.Published)
        {
            return;
        }
        ValidatePublisher(publisher);

        Status = ArticleStatus.Published;
        Publisher = publisher;
        PublisherId = publisher.Id;
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
        Publisher = null;
        PublisherId = null;
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
        MarkAsUpdated();
    }

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

        if (title.Length < TitleMinLength)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.TitleMustBeAtLeastXCharactersLong, TitleMinLength),
                nameof(title)
            );
        }

        if (title.Length > TitleMaxLength)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.TitleCantExceedXCharacters, TitleMaxLength),
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

    private void ValidateCreator(User creator)
    {
        ArgumentNullException.ThrowIfNull(creator, nameof(creator));
    }

    private void ValidatePublisher(User publisher)
    {
        ArgumentNullException.ThrowIfNull(publisher, nameof(publisher));
    }

    [GeneratedRegex("[^a-z0-9-]")]
    private static partial Regex DashedAlphanumericRegex();

    [GeneratedRegex("-{2,}")]
    private static partial Regex MultiDashRegex();
}
