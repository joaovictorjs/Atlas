using Atlas.Domain.Entities;
using Atlas.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Data.EntityTypeConfigurations
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id).HasColumnName("id");

            builder
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(User.NameMaxLength)
                .HasColumnName("name");

            builder
                .Property(user => user.PhotoUrl)
                .IsRequired()
                .HasMaxLength(User.PhotoUrlMaxLength)
                .HasColumnName("photo_url");

            builder
                .Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(User.EmailMaxLength)
                .HasColumnName("email");

            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.PasswordHash).IsRequired().HasColumnName("password_hash");

            builder
                .Property(user => user.Role)
                .IsRequired()
                .HasConversion(roleEnum => roleEnum.ToString(), str => Enum.Parse<UserRole>(str))
                .HasColumnName("role");

            builder.Property(user => user.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(user => user.UpdatedAt).IsRequired().HasColumnName("updated_at");
        }
    }
}
