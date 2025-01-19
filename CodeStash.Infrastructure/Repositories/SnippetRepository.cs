using CodeStash.Application.Repositories;
using CodeStash.Core.Entities;
using CodeStash.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodeStash.Infrastructure.Repositories;
internal class SnippetRepository(ApplicationDbContext context) : ISnippetRepository
{
    public async Task<int> AddAsync(Snippet snippet)
    {
        await context.Snippets.AddAsync(snippet);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Snippet snippet)
    {
        context.Snippets.Update(snippet);
        return await context.SaveChangesAsync();
    }

    public IQueryable<Snippet> GetUserSnippets(string userId)
    {
        return context.Snippets
            .Include(s => s.Tags)
            .Include(s => s.User)
            .Where(s => s.UserId == userId)
            .AsQueryable();
    }

    public IQueryable<Snippet> GetAllSnippets()
    {
        return context.Snippets
            .Include(s => s.Tags)
            .Include(s => s.User)
            .AsQueryable();
    }

    public async Task<int> DeleteAsync(Snippet snippet)
    {
        context.Snippets.Remove(snippet);
        return await context.SaveChangesAsync();
    }

    public async Task<Snippet?> GetByIdAsync(string snippetId)
    {
        return await context.Snippets.FindAsync(snippetId);
    }
}
