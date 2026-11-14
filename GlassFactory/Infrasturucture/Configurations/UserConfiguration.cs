using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrasturucture.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.PasswordHash)
                .IsRequired();
            builder.Property(u => u.CreatedOnUtc)
                .IsRequired();
            builder.Property(u => u.UpdatedOnUtc);
            builder.Property(u => u.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.UpdatedBy)
                .HasMaxLength(50);
            builder.HasMany<Role>(u => u.Roles)
                .WithMany(r => r.Users);
        }


    }
}
