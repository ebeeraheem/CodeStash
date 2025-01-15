namespace CodeStash.Application.Filters;
public sealed class SnippetsFilter
{
    public string? Title { get; set; }
    public string? Language { get; set; }
    public string? AuthorUserName { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
