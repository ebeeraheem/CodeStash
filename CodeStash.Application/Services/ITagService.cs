using CodeStash.Application.Models;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public interface ITagService
{
    Task<Result> AddTagAsync(TagDto request);
    Task<Result> DeleteTagAsync(Guid tagId);
    Task<Result> GetAllTags();
    Task<Result> UpdateTagAsync(Guid tagId, TagDto request);
}