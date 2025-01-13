using CodeStash.Application.Models;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public interface ITagService
{
    Task<Result> AddTagAsync(TagDto request);
    Task<Result> DeleteTagAsync(Guid id);
    Task<Result> GetAllTags();
    Task<Result> UpdateTagAsync(Guid id, TagDto request);
}