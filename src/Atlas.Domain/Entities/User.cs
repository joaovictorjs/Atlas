using System.Net.Mail;
using Atlas.Domain.Enums;
using Atlas.Exceptions;
using Atlas.Exceptions.Resources;

namespace Atlas.Domain.Entities;

public class User : BaseEntity
{
    public const int NameMinLength = 4;
    public const int NameMaxLength = 100;
    public const int PhotoUrlMaxLength = 2048;
    public const int EmailMaxLength = 255;

    public string Name { get; private set; } = string.Empty;
    public string PhotoUrl { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }

    // Navigation
    public ICollection<Tag> CreatedTags { get; private set; } = [];
    public ICollection<Category> CreatedCategories { get; private set; } = [];
    public ICollection<Article> CreatedArticles { get; private set; } = [];
    public ICollection<Article> PublishedArticles { get; private set; } = [];

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

    private User() { }

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
        DomainException.ThrowIfNullOrWhiteSpace(name, ExceptionMessages.NameCantBeNullOrWhiteSpace);

        DomainException.ThrowIfOutOfRange(
            name.Length,
            NameMinLength,
            NameMaxLength,
            string.Format(ExceptionMessages.NameMustBeAtLeastXCharactersLong, NameMinLength),
            string.Format(ExceptionMessages.NameCantExceedXCharacters, NameMaxLength)
        );
    }

    private static void ValidatePhotoUrl(string photoUrl)
    {
        DomainException.ThrowIfNullOrWhiteSpace(
            photoUrl,
            ExceptionMessages.PhotoUrlCantBeNullOrWhiteSpace
        );

        DomainException.ThrowIfGreaterThan(
            photoUrl.Length,
            PhotoUrlMaxLength,
            string.Format(ExceptionMessages.PhotoUrlCantExceedXCharacters, PhotoUrlMaxLength)
        );

        if (
            !Uri.TryCreate(photoUrl, UriKind.Absolute, out var uri)
            || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
        )
        {
            throw new DomainException(ExceptionMessages.PhotoUrlMustBeValidHttpOrHttps);
        }
    }

    private static void ValidateEmail(string email)
    {
        DomainException.ThrowIfNullOrWhiteSpace(
            email,
            ExceptionMessages.EmailCantBeNullOrWhiteSpace
        );

        DomainException.ThrowIfGreaterThan(
            email.Length,
            EmailMaxLength,
            string.Format(ExceptionMessages.EmailCantExceedXCharacters, EmailMaxLength)
        );

        if (!MailAddress.TryCreate(email, out var _))
        {
            throw new DomainException(ExceptionMessages.EmailMustBeValidEmailAddress);
        }
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        DomainException.ThrowIfNullOrWhiteSpace(
            passwordHash,
            ExceptionMessages.PasswordHashCantBeNullOrWhiteSpace
        );
    }
}
