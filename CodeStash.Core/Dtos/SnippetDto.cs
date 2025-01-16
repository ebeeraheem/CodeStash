namespace CodeStash.Core.Dtos;
public class SnippetDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public List<TagDto> Tags { get; set; } = [];
    public bool IsPrivate { get; set; } = false;
    public int ViewCount { get; set; }
    public string AuthorUserName { get; set; } = string.Empty;
}
