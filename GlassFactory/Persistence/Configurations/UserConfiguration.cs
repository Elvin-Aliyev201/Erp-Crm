using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
        builder.Property(u => u.UserName).HasMaxLength(50).IsRequired();
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.Property(u => u.FullName).HasMaxLength(150).IsRequired();
        builder.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
        builder.Property(u => u.IsActive).IsRequired();

        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity(
                "user_roles",
                r => r.HasOne(typeof(Role)).WithMany().HasForeignKey("role_id").OnDelete(DeleteBehavior.Restrict),
                l => l.HasOne(typeof(User)).WithMany().HasForeignKey("user_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey("user_id", "role_id"));

        builder.Navigation(u => u.Roles).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.ConfigureAudit();
    }
}
