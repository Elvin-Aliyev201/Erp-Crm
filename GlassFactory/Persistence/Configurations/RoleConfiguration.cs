using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(r => r.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(r => r.Name).IsUnique();
        builder.Property(r => r.Description).HasMaxLength(250).IsRequired();

        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity(
                "role_permissions",
                r => r.HasOne(typeof(Permission)).WithMany().HasForeignKey("permission_id").OnDelete(DeleteBehavior.Restrict),
                l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("role_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey("role_id", "permission_id"));

        builder.Navigation(r => r.Permissions).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.ConfigureAudit();
    }
}
