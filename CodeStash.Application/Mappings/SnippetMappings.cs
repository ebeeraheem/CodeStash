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
            Description = snippet.Description,
            Slug = snippet.Slug,
            Content = snippet.Content,
            Language = snippet.Language,
            Tags = snippet.Tags?.Select(t => new TagDto { Id = t.Id, Name = t.Name}).ToList(),
            ViewCount = snippet.ViewCount,
            IsPrivate = snippet.IsPrivate,
            AuthorId = snippet.UserId,
            AuthorDisplayName = snippet.User?.UserName ?? string.Empty
        };
    }
}
