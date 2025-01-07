using System.ComponentModel.DataAnnotations;

namespace CodeStash.Infrastructure.Audit;
public class AuditLog
{
    public int Id { get; set; }

    [MaxLength(450)]
    public string? UserId { get; set; }

    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;

    [MaxLength(200)]
    public string EntityName { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; }

    [MaxLength(4000)]
    public string? OldValues { get; set; }

    [MaxLength(4000)]
    public string? NewValues { get; set; }

    [MaxLength(500)]
    public string? AffectedColumns { get; set; }

    [MaxLength(450)]
    public string? PrimaryKey { get; set; }
}
