using CodeStash.Core;
using CodeStash.Core.Interfaces;
using CodeStash.Infrastructure.Audit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeStash.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions options, IAuditService auditService) : IdentityDbContext(options)
{
    public required DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public required DbSet<ApplicationRole> ApplicationRoles { get; set; }

    public required DbSet<AuditLog> AuditLogs { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Apply audit trail before saving
        await auditService.ApplyAuditTrailAsync();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply configuration to all entities that implement IAuditableEntity
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType)) continue;

            builder.Entity(entityType.ClrType)
                .Property(nameof(IAuditableEntity.CreatedBy))
                .HasMaxLength(150);

            builder.Entity(entityType.ClrType)
                .Property(nameof(IAuditableEntity.ModifiedBy))
                .HasMaxLength(150);
        }

        base.OnModelCreating(builder);
    }
}
