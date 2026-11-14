using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal static class AuditableEntityConfiguration
{
    public static void ConfigureAudit<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity
    {
        builder.Property(e => e.CreatedOnUtc).IsRequired();
        builder.Property(e => e.CreatedBy).HasMaxLength(64).IsRequired();
        builder.Property(e => e.UpdatedBy).HasMaxLength(64);
    }
}
