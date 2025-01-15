using CodeStash.Application.Repositories;
using CodeStash.Core.Entities;
using CodeStash.Infrastructure.Persistence;

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

    public async Task<int> UpdateAsync(Tag tag)
    {
        context.Tags.Update(tag);
        return await context.SaveChangesAsync();
    }
}
