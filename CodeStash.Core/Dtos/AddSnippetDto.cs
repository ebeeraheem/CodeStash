using CodeStash.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Dtos;
public class AddSnippetDto
{
    [MaxLength(200)]
    public required string Title { get; set; }

    [MaxLength(4000)]
    public required string Content { get; set; }
    public required string Language { get; set; }
    public List<string>? TagIds { get; set; } = [];
}
