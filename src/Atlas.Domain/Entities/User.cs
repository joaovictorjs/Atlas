using System.Net.Mail;
using Atlas.Domain.Enums;
using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string PhotoUrl { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }

    public User(string name, string photoUrl, string email, string passwordHash)
    {
        ValidateName(name);
        ValidatePhotoUrl(photoUrl);
        ValidateEmail(email);
        ValidatePasswordHash(passwordHash);

        Name = name;
        PhotoUrl = photoUrl;
        Email = email;
        PasswordHash = passwordHash;
        Role = UserRole.Reader;
    }

    public void ChangeName(string name)
    {
        ValidateName(name);
        Name = name;
        MarkAsUpdated();
    }

    public void ChangePhotoUrl(string photoUrl)
    {
        ValidatePhotoUrl(photoUrl);
        PhotoUrl = photoUrl;
        MarkAsUpdated();
    }

    public void ChangeEmail(string email)
    {
        ValidateEmail(email);
        Email = email;
        MarkAsUpdated();
    }

    public void ChangePasswordHash(string passwordHash)
    {
        ValidatePasswordHash(passwordHash);
        PasswordHash = passwordHash;
        MarkAsUpdated();
    }

    public void SetAsAdmin()
    {
        if (Role != UserRole.Admin)
        {
            Role = UserRole.Admin;
            MarkAsUpdated();
        }
    }

    public void SetAsReader()
    {
        if (Role != UserRole.Reader)
        {
            Role = UserRole.Reader;
            MarkAsUpdated();
        }
    }

    public void SetAsWriter()
    {
        if (Role != UserRole.Writer)
        {
            Role = UserRole.Writer;
            MarkAsUpdated();
        }
    }

    private static void ValidateName(string name)
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
                string.Format(ExceptionMessages.NameCantExceedXCharacters),
                nameof(name)
            );
        }
    }

    private static void ValidatePhotoUrl(string photoUrl)
    {
        if (string.IsNullOrWhiteSpace(photoUrl))
        {
            throw new ArgumentException(
                ExceptionMessages.PhotoUrlCantBeNullOrWhiteSpace,
                nameof(photoUrl)
            );
        }

        const int MaxLenght = 2048;
        if (photoUrl.Length > MaxLenght)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.PhotoUrlCantExceedXCharacters, MaxLenght),
                nameof(photoUrl)
            );
        }

        if (
            !Uri.TryCreate(photoUrl, UriKind.Absolute, out var uri)
            || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
        )
        {
            throw new ArgumentException(
                ExceptionMessages.PhotoUrlMustBeValidHttpOrHttps,
                nameof(photoUrl)
            );
        }
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(
                ExceptionMessages.EmailCantBeNullOrWhiteSpace,
                nameof(email)
            );
        }

        const int MaxLength = 255;
        if (email.Length > MaxLength)
        {
            throw new ArgumentException(
                string.Format(ExceptionMessages.EmailCantExceedXCharacters, MaxLength),
                nameof(email)
            );
        }

        if (!MailAddress.TryCreate(email, out var _))
        {
            throw new ArgumentException(
                ExceptionMessages.EmailMustBeValidEmailAddress,
                nameof(email)
            );
        }
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException(
                ExceptionMessages.PasswordHashCantBeNullOrWhiteSpace,
                nameof(passwordHash)
            );
        }
    }

    private User() { }
}
