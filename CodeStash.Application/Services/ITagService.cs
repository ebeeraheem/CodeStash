using CodeStash.Application.Models;

namespace CodeStash.Application.Services;
public interface ITagService
{
    Task<Result> AddTagAsync(TagModel request);
    Task<Result> DeleteTagAsync(string tagId);
    Task<Result> GetAllTags();
    Task<Result> UpdateTagAsync(string tagId, TagModel request);
}