using CodeStash.Application.Repositories;
using CodeStash.Core.Entities;
using CodeStash.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodeStash.Infrastructure.Repositories;
internal class TagRepository(ApplicationDbContext context) : ITagRepository
{
    public async Task<int> AddAsync(Tag tag)
    {
        await context.Tags.AddAsync(tag);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Tag tag)
    {
        context.Tags.Remove(tag);
        return await context.SaveChangesAsync();
    }

    public IQueryable<Tag> GetAllTags()
    {
        return context.Tags;
    }

    public async Task<Tag?> GetByIdAsync(Guid tagId)
    {
        return await context.Tags.FindAsync(tagId);
    }

    public async Task<bool> IsValidTag(Guid tagId)
    {
        return await context.Tags.AnyAsync(t => t.Id == tagId);
    }

    public async Task<Tag?> GetTagWithSnippets(Guid tagId)
    {
        return await context.Tags
            .Include(t => t.Snippets)
            .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(t => t.Id == tagId);
    }

    public async Task<int> UpdateAsync(Tag tag)
    {
        context.Tags.Update(tag);
        return await context.SaveChangesAsync();
    }
}
