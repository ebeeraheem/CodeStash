using CodeStash.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Entities;
public class ApplicationUser : IdentityUser, IAuditableEntity
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [MaxLength(50)]
    public string Role { get; set; } = string.Empty;
    public DateTime LastLoginDate { get; set; }

    public List<Snippet> Snippets { get; set; } = [];

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
