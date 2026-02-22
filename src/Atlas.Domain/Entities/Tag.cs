using Atlas.Exceptions;
using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public class Tag : BaseEntity
{
    public const int NameMinLength = 4;
    public const int NameMaxLength = 100;

    public int CreatorId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    // Navigation
    public User Creator { get; private set; } = null!;
    public ICollection<ArticleTag> ArticleTags { get; private set; } = null!;

    public Tag(string name, User creator)
    {
        ValidateName(name);
        ValidateCreator(creator);

        Name = name;
        Creator = creator;
        CreatorId = Creator.Id;
    }

    private Tag() { }

    public void ChangeName(string name)
    {
        ValidateName(name);

        Name = name;
        MarkAsUpdated();
    }

    private static void ValidateName(string name)
    {
        DomainException.ThrowIfNullOrWhiteSpace(name, ExceptionMessages.NameCantBeNullOrWhiteSpace);

        DomainException.ThrowIfOutOfRange(
            name.Length,
            NameMinLength,
            NameMaxLength,
            string.Format(ExceptionMessages.NameMustBeAtLeastXCharactersLong, NameMinLength),
            string.Format(ExceptionMessages.NameCantExceedXCharacters, NameMaxLength)
        );
    }

    private static void ValidateCreator(User creator)
    {
        DomainException.ThrowIfNull(creator, ExceptionMessages.CreatorCantBeNull);
    }
}
