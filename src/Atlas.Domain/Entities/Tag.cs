using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public class Tag : BaseEntity
{
    public const int NameMinLength = 4;
    public const int NameMaxLength = 100;

    public string Name { get; private set; } = string.Empty;

    public Tag(string name)
    {
        ValidateName(name);
        Name = name;
    }

    private Tag() { }

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
}
