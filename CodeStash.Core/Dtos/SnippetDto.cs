using CodeStash.Core.Entities;

namespace CodeStash.Core.Dtos;
public class SnippetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public List<Tag> Tags { get; set; } = [];
    public bool IsPrivate { get; set; } = false;
    public int ViewCount { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
