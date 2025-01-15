using CodeStash.Core.Entities;

namespace CodeStash.Application.Repositories;
public interface ISnippetRepository
{
    Task<int> AddAsync(Snippet snippet);
    Task<int> UpdateAsync(Snippet snippet);
    Task<int> DeleteAsync(Snippet snippet);
    Task<Snippet?> GetByIdAsync(Guid snippetId);
    IQueryable<Snippet> GetAllSnippets();
    IQueryable<Snippet> GetSnippetsWithAuthor();
    IQueryable<Snippet> GetByUserAsync(string userId);
}
