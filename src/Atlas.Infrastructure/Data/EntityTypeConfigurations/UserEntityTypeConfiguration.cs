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

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("id");

            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(User.NameMaxLength)
                .HasColumnName("name");

            builder
                .Property(u => u.PhotoUrl)
                .IsRequired()
                .HasMaxLength(User.PhotoUrlMaxLength)
                .HasColumnName("photo_url");

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(User.EmailMaxLength)
                .HasColumnName("email");

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.PasswordHash).IsRequired().HasColumnName("password_hash");

            builder
                .Property(u => u.Role)
                .IsRequired()
                .HasConversion(to => to.ToString(), from => Enum.Parse<UserRole>(from))
                .HasColumnName("role");

            builder.Property(u => u.CreatedAt).IsRequired().HasColumnName("created_at");

            builder.Property(u => u.UpdatedAt).IsRequired().HasColumnName("updated_at");
        }
    }
}
