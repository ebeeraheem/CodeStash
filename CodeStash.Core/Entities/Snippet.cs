using CodeStash.Core.Interfaces;
using CodeStash.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeStash.Core.Entities;

[Index(nameof(Language))]
public class Snippet : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(200)]
    public required string Title { get; set; }

    [MaxLength(4000)]
    public required string Content { get; set; }

    [MaxLength(100)]
    public required string Language { get; set; }
    public List<Tag>? Tags { get; set; } = [];
    public bool IsPrivate { get; set; } = false;
    public int ViewCount { get; set; }

    [ForeignKey(nameof(User))]
    public required string UserId { get; set; }
    public required ApplicationUser User { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}