using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Dtos;

public class UpdateSnippetDto
{
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(4000)]
    public string? Content { get; set; }
    public string? Language { get; set; }
    public List<string>? TagIds { get; set; } = [];
}