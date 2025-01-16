using CodeStash.Core.Entities;

namespace CodeStash.Application.Repositories;
public interface ITagRepository
{
    Task<int> AddAsync(Tag tag);
    Task<int> UpdateAsync(Tag tag);
    Task<int> DeleteAsync(Tag tag);
    Task<Tag?> GetByIdAsync(Guid tagId);
    IQueryable<Tag> GetAllTags();
    Task<Tag?> GetTagWithSnippets(Guid tagId);
    Task<bool> IsValidTag(Guid tagId);
}
