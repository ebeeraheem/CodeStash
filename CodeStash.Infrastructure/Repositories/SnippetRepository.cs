using CodeStash.Application.Repositories;
using CodeStash.Core.Entities;
using CodeStash.Infrastructure.Persistence;

namespace CodeStash.Infrastructure.Repositories;
internal class SnippetRepository(ApplicationDbContext context) : ISnippetRepository
{
    public async Task<int> AddAsync(Snippet snippet)
    {
        await context.Snippets.AddAsync(snippet);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Snippet snippet)
    {
        context.Snippets.Remove(snippet);
        return await context.SaveChangesAsync();
    }

    public IQueryable<Snippet> GetAllSnippets()
    {
        return context.Snippets;
    }

    public async Task<Snippet?> GetByIdAsync(Guid id)
    {
        return await context.Snippets.FindAsync(id);
    }

    public async Task<int> UpdateAsync(Snippet snippet)
    {
        context.Snippets.Update(snippet);
        return await context.SaveChangesAsync();
    }
}
