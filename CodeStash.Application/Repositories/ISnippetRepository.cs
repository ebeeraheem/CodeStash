using CodeStash.Core.Entities;

namespace CodeStash.Application.Repositories;
public interface ISnippetRepository
{
    Task<int> AddAsync(Snippet snippet);
    Task<int> UpdateAsync(Snippet snippet);
    Task<int> DeleteAsync(Snippet snippet);
    Task<Snippet?> GetByIdAsync(string snippetId);
    IQueryable<Snippet> GetAllSnippets();
    IQueryable<Snippet> GetUserSnippets(string userId);
}
