using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Entities;

[Index(nameof(Name))]
public class Tag
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(50)]
    public required string Name { get; set; }
    public ICollection<Snippet> Snippets { get; set; } = [];
}