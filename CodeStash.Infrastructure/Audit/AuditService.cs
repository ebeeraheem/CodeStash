using CodeStash.Core.Interfaces;
using CodeStash.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CodeStash.Infrastructure.Audit;
internal class AuditService(
    IServiceProvider serviceProvider,
    IHttpContextAccessor httpContextAccessor) : IAuditService
{
    public async Task ApplyAuditTrailAsync()
    {
        var userId = httpContextAccessor?.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var context = serviceProvider.GetService<ApplicationDbContext>();
        if (context is null)
            throw new InvalidOperationException("No database context was found.");

        var changeTracker = context.ChangeTracker;
        changeTracker.DetectChanges();

        // Create a list of audit entries
        var auditEntries = new List<AuditEntry>();

        foreach (var entry in changeTracker.Entries())
        {
            // Skip iteration if entity state is detached or unchanged
            // Or if entity is AuditLog
            if (entry.Entity is AuditLog || entry.State is EntityState.Detached or EntityState.Unchanged)
                continue;

            // Create a new audit entry based on the current entity
            var auditEntry = new AuditEntry(entry)
            {
                UserId = userId,
                EntityName = entry.Entity.GetType().Name
            };

            // Iterate through all properties of the current entity
            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;

                // Skip iteration if current property is a primary key
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.PrimaryKey = property.CurrentValue?.ToString();
                    continue;
                }

                // Populate audit fields in auditable properties
                if (entry.Entity is IAuditableEntity auditableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.CreatedAt = DateTime.UtcNow;
                            auditableEntity.CreatedBy = userId ?? string.Empty;
                            break;
                        case EntityState.Modified:
                            auditableEntity.ModifiedAt = DateTime.UtcNow;
                            auditableEntity.ModifiedBy = userId;
                            break;
                    }
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        // Add new values if audit type is create
                        auditEntry.AuditType = EAuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;

                    case EntityState.Deleted:
                        // Add deleted values if audit type is delete
                        auditEntry.AuditType = EAuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;

                    case EntityState.Modified:
                        // Add both old and new values if audit type is update
                        if (property.IsModified && !Equals(property.CurrentValue, property.OriginalValue))
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = EAuditType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }

            // Add the audit entry to the list of audit entries
            // if it is a valid audit type
            if (auditEntry.AuditType is not EAuditType.None)
            {
                auditEntries.Add(auditEntry);
            }
        }

        var auditLogs = auditEntries
            .Select(auditEntry => auditEntry.ToAuditLog())
            .ToList();

        await context.AuditLogs.AddRangeAsync(auditLogs);
    }
}
