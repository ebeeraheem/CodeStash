using CodeStash.Application.Models;
using CodeStash.Core.Entities;
using CodeStash.Core.Models;

namespace CodeStash.Application.Services;
public interface ITagService
{
    Task<Result> AddTagAsync(TagModel request);
    Task<Result> DeleteTagAsync(string tagId);
    Task<Result<List<Tag>>> GetAllTags();
    Task<Result> UpdateTagAsync(string tagId, TagModel request);
}