using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasturucture.Configurations
{
    internal class PermissionConfiguration: IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Description)
                .HasMaxLength(200);
            builder.HasMany<Role>(p => p.Roles)
                .WithMany(r => r.Permissions);
        }
    }
}
