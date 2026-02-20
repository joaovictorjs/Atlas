using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public const int NameMinLength = 4;
    public const int NameMaxLength = 100;

    public Category(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void ChangeName(string name)
    {
        ValidateName(name);
        Name = name;
        MarkAsUpdated();
    }

    private Category() { }

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
}
