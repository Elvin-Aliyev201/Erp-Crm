using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasturucture.Configurations
{
    internal class RoleConfiguration: IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.Description)
                .HasMaxLength(200);
            builder.Property(r => r.CreatedOnUtc)
                .IsRequired();
            builder.Property(r => r.UpdatedOnUtc);
            builder.Property(r => r.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.UpdatedBy)
                .HasMaxLength(50);
            builder.HasMany<User>(r => r.Users)
                .WithMany(u => u.Roles);
            builder.HasMany<Permission>(r => r.Permissions)
                .WithMany(p => p.Roles);
        }

    }
}
