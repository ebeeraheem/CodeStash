using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace CodeStash.Infrastructure.Audit;
internal class AuditEntry(EntityEntry entry)
{
    public EntityEntry Entry { get; } = entry;
    public string? UserId { get; set; }
    public required string EntityName { get; set; }
    public string? PrimaryKey { get; set; }
    public Dictionary<string, object?> OldValues { get; } = new();
    public Dictionary<string, object?> NewValues { get; } = new();
    public EAuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = [];

    public AuditLog ToAuditLog()
    {
        return new AuditLog
        {
            UserId = UserId,
            Type = AuditType.ToString(),
            EntityName = EntityName,
            TimeStamp = DateTime.UtcNow,
            PrimaryKey = PrimaryKey,
            OldValues = JsonSerializer.Serialize(OldValues),
            NewValues = JsonSerializer.Serialize(NewValues),
            AffectedColumns = JsonSerializer.Serialize(ChangedColumns)
        };
    }
}
