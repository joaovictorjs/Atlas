using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

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

        const int MinLength = 4;
        if (name.Length < MinLength)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.NameMustBeAtLeastXCharactersLong, MinLength),
                nameof(name)
            );
        }

        const int MaxLength = 100;
        if (name.Length > MaxLength)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.NameCantExceedXCharacters, MaxLength),
                nameof(name)
            );
        }
    }
}
