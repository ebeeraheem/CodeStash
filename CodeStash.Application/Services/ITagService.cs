using CodeStash.Application.Models;

namespace CodeStash.Application.Services;
public interface ITagService
{
    Task<Result> AddTagAsync(TagModel request);
    Task<Result> DeleteTagAsync(Guid tagId);
    Task<Result> GetAllTags();
    Task<Result> UpdateTagAsync(Guid tagId, TagModel request);
}