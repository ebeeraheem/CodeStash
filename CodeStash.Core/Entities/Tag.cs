using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Entities;

[Index(nameof(Name))]
public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    public required string Name { get; set; }
    public ICollection<Snippet> Snippets { get; set; } = [];
}