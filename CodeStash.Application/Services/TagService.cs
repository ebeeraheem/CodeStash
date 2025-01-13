using CodeStash.Application.Errors;
using CodeStash.Application.Models;
using CodeStash.Application.Repositories;
using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeStash.Application.Services;
public class TagService(ITagRepository tagRepository) : ITagService
{
    public async Task<Result> AddTagAsync(TagDto request)
    {
        var tag = new Tag { Name = request.Name };
        var result = await tagRepository.AddAsync(tag);

        if (result <= 0)
        {
            return Result.Failure(TagErrors.CannotCreate);
        }

        return Result.Success();
    }

    public async Task<Result> UpdateTagAsync(Guid id, TagDto request)
    {
        var tag = await tagRepository.GetByIdAsync(id);

        if (tag is null)
        {
            return Result.Failure(TagErrors.TagNotFound);
        }

        tag.Name = request.Name;

        var result = await tagRepository.UpdateAsync(tag);

        if (result <= 0)
        {
            return Result.Failure(TagErrors.FailedToSave);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteTagAsync(Guid id)
    {
        var tag = await tagRepository.GetByIdAsync(id);

        if (tag is null)
        {
            return Result.Failure(TagErrors.TagNotFound);
        }

        var result = await tagRepository.DeleteAsync(tag);

        if (result <= 0)
        {
            return Result.Failure(TagErrors.FailedToDelete);
        }

        return Result.Success();
    }

    public async Task<Result> GetAllTags()
    {
        var tags = await tagRepository.GetAllTags().ToListAsync();

        return Result<List<Tag>>.Success(tags);
    }
}
