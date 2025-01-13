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

    public IQueryable<Tag> GetAll()
    {
        return context.Tags;
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        return await context.Tags.FindAsync(id);
    }

    public async Task<int> UpdateAsync(Tag tag)
    {
        context.Tags.Update(tag);
        return await context.SaveChangesAsync();
    }
}
