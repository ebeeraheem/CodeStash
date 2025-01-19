using CodeStash.Core.Entities;

namespace CodeStash.Application.Repositories;
public interface ITagRepository
{
    Task<int> AddAsync(Tag tag);
    Task<int> UpdateAsync(Tag tag);
    Task<int> DeleteAsync(Tag tag);
    Task<Tag?> GetByIdAsync(string tagId);
    IQueryable<Tag> GetAllTags();
}
