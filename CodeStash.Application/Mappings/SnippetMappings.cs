using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;

namespace CodeStash.Application.Mappings;
public static class SnippetMappings
{
    public static SnippetDto ToSnippetDto(this Snippet snippet)
    {
        return new SnippetDto()
        {
            Id = snippet.Id,
            Title = snippet.Title,
            Content = snippet.Content,
            Language = snippet.Language,
            Tags = snippet.Tags?.Select(t => new TagDto { Id=t.Id, Name=t.Name}).ToList(),
            ViewCount = snippet.ViewCount,
            IsPrivate = snippet.IsPrivate,
            AuthorUserName = snippet.User?.UserName ?? string.Empty
        };
    }
}
