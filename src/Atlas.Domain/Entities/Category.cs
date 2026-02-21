using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public class Category : BaseEntity
{
    public const int NameMinLength = 4;
    public const int NameMaxLength = 100;

    public int CreatorId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    // Navigation
    public User Creator { get; private set; } = null!;
    public ICollection<Article> Articles { get; private set; } = [];

    public Category(string name, User creator)
    {
        ValidateName(name);
        ValidateCreator(creator);

        Name = name;
        Creator = creator;
        CreatorId = Creator.Id;
    }

    private Category() { }

    public void ChangeName(string name)
    {
        ValidateName(name);
        Name = name;
        MarkAsUpdated();
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(ExceptionMessages.NameCantBeNullOrWhiteSpace, nameof(name));
        }

        if (name.Length < NameMinLength)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.NameMustBeAtLeastXCharactersLong, NameMinLength),
                nameof(name)
            );
        }

        if (name.Length > NameMaxLength)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.NameCantExceedXCharacters, NameMaxLength),
                nameof(name)
            );
        }
    }

    private void ValidateCreator(User creator)
    {
        ArgumentNullException.ThrowIfNull(creator, nameof(creator));
    }
}
