using CodeStash.Application.Errors;
using CodeStash.Application.Filters;
using CodeStash.Application.Mappings;
using CodeStash.Application.Repositories;
using CodeStash.Application.Utilities;
using CodeStash.Application.Utilities.Pagination;
using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;
using CodeStash.Core.Models;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeStash.Application.Services;
public class SnippetService(ISnippetRepository snippetRepository,
                            UserManager<ApplicationUser> userManager,
                            ITagRepository tagRepository,
                            IPagedResultService pagedResultService,
                            UserHelper userHelper) : ISnippetService
{
    public async Task<Result> AddSnippetAsync(AddSnippetDto request)
    {
        const int MaxTags = 5;
        var userId = userHelper.GetUserId();

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        if (!SnippetLanguage.IsValid(request.Language))
        {
            return Result.Failure(SnippetErrors.InvalidLanguage);
        }

        if (string.IsNullOrWhiteSpace(request.Title) ||
            string.IsNullOrWhiteSpace(request.Content))
        {
            return Result.Failure(SnippetErrors.InvalidTitleOrContent);
        }

        if (request.TagIds?.Count > MaxTags)
        {
            return Result.Failure(SnippetErrors.MaximumTagsExceeded);
        }

        // check if request.TagIds is null: valid
        if (request.TagIds is null)
        {
            request.TagIds = [];
        }
        // check if request.TagIds.Count is 1 and the value is an empty string: valid
        else if (request.TagIds.Count == 1 && request.TagIds.Single() == string.Empty)
        {
            request.TagIds = [];
        }
        // check if request.TagIds contains an empty string when there are other values: invalid
        else if (request.TagIds.Any(tagId => string.IsNullOrEmpty(tagId)))
        {
            return Result.Failure(SnippetErrors.InvalidTags);
        }

        var tags = await tagRepository.GetAllTags()
            .Where(tag => request.TagIds != null && request.TagIds.Contains(tag.Id))
            .ToListAsync();

        // check if all provided tag IDs exist in the repository
        if (request.TagIds.Any())
        {
            var invalidTagIds = request.TagIds
                .Except(tags.Select(tag => tag.Id))
                .ToList();

            if (invalidTagIds.Any())
            {
                return Result.Failure(SnippetErrors.InvalidTags);
            }
        }

        var snippet = new Snippet()
        {
            Title = request.Title,
            Content = Markdown.ToHtml(request.Content),
            Language = request.Language,
            UserId = userId,
            User = user,
            Tags = tags
        };

        var result = await snippetRepository.AddAsync(snippet);

        if (result <= 0)
        {
            return Result.Failure(SnippetErrors.CannotCreate);
        }

        return Result.Success();
    }

    public Result<List<string?>> GetLanguages()
    {
        var languages = LanguageCategories.GetAllLanguages();

        return Result<List<string?>>.Success(languages);
    }

    public Result<Dictionary<string, List<string?>>> GetAllCategoriesWithLanguages()
    {
        var languages = LanguageCategories.GetAllCategoriesWithLanguages();

        return Result<Dictionary<string, List<string?>>>.Success(languages);
    }

    public Result<List<string?>> GetLanguagesByCategory(string category)
    {
        var languages = LanguageCategories.GetCategoryLanguages(category);

        return Result<List<string?>>.Success(languages);
    }

    public Result<List<string?>> GetAllSnippetLanguages()
    {
        var languages = SnippetLanguage.GetAll();

        return Result<List<string?>>.Success(languages);
    }

    // ALWAYS INCLUDE A SNIPPET'S TAG IDS DURING UPDATES
    // TO AVOID ACCIDENTALLY REMOVING THEM!!!
    public async Task<Result> UpdateSnippetAsync(string snippetId, UpdateSnippetDto request)
    {
        const int MaxTags = 5;
        var userId = userHelper.GetUserId();

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        var snippet = await snippetRepository.GetByIdAsync(snippetId);

        if (snippet is null)
        {
            return Result.Failure(SnippetErrors.SnippetNotFound);
        }

        if (snippet.UserId != userId)
        {
            return Result.Failure(SnippetErrors.CannotModify);
        }

        if (request.TagIds?.Count > MaxTags)
        {
            return Result.Failure(SnippetErrors.MaximumTagsExceeded);
        }

        snippet.Title = !string.IsNullOrWhiteSpace(request.Title)
            ? request.Title
            : snippet.Title;

        snippet.Content = !string.IsNullOrWhiteSpace(request.Content)
            ? Markdown.ToHtml(request.Content)
            : snippet.Content;

        if (!string.IsNullOrWhiteSpace(request.Language))
        {
            if (!SnippetLanguage.IsValid(request.Language))
            {
                return Result.Failure(SnippetErrors.InvalidLanguage);
            }

            snippet.Language = request.Language;
        }

        // check if request.TagIds is null: valid
        if (request.TagIds is null)
        {
            request.TagIds = [];
        }
        // check if request.TagIds.Count is 1 and the value is an empty string: valid
        else if (request.TagIds.Count == 1 && request.TagIds.Single() == string.Empty)
        {
            request.TagIds = [];
        }
        // check if request.TagIds contains an empty string when there are other values: invalid
        else if (request.TagIds.Any(tagId => string.IsNullOrEmpty(tagId)))
        {
            return Result.Failure(SnippetErrors.InvalidTags);
        }

        var tags = await tagRepository.GetAllTags()
            .Where(tag => request.TagIds != null && request.TagIds.Contains(tag.Id))
            .ToListAsync();

        // check if all provided tag IDs exist in the repository
        if (request.TagIds.Any())
        {
            var invalidTagIds = request.TagIds
                .Except(tags.Select(tag => tag.Id))
                .ToList();

            if (invalidTagIds.Any())
            {
                return Result.Failure(SnippetErrors.InvalidTags);
            }
        }

        snippet.Tags = tags;

        var result = await snippetRepository.UpdateAsync(snippet);

        if (result <= 0)
        {
            return Result.Failure(SnippetErrors.UpdateFailed);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteSnippetAsync(string snippetId)
    {
        var snippet = await snippetRepository.GetByIdAsync(snippetId);

        if (snippet is null)
        {
            return Result.Failure(SnippetErrors.SnippetNotFound);
        }

        var userId = userHelper.GetUserId();

        if (snippet.UserId != userId)
        {
            return Result.Failure(SnippetErrors.CannotModify);
        }

        var result = await snippetRepository.DeleteAsync(snippet);

        if (result <= 0)
        {
            return Result.Failure(SnippetErrors.DeleteFailed);
        }

        return Result.Success();
    }

    public async Task<Result> GetSnippets(int pageNumber, int pageSize, SnippetsFilter filter)
    {
        var snippets = snippetRepository.GetAllSnippets();
        var publicSnippets = snippets.Where(s => !s.IsPrivate);
        var filtered = ApplyFilter(publicSnippets, filter);
        var ordered = filtered.OrderByDescending(s => s.ViewCount);
        var snippetDtos = ordered.Select(s => s.ToSnippetDto());

        var paginated = await pagedResultService
            .GetPagedResultAsync(snippetDtos, pageNumber, pageSize);

        return Result<PagedResult<SnippetDto>>.Success(paginated);
    }

    public async Task<Result> GetMySnippets()
    {
        var userId = userHelper.GetUserId();
        var snippets = await snippetRepository.GetUserSnippets(userId)
            .OrderByDescending(s => s.ViewCount)
            .Select(s => s.ToSnippetDto())
            .ToListAsync();

        return Result<List<SnippetDto>>.Success(snippets);
    }

    public async Task<Result> GetSnippetsByAuthorId(string authorId)
    {
        var snippets = await snippetRepository.GetAllSnippets()
            .Where(s => !s.IsPrivate && s.UserId == authorId)
            .OrderByDescending(s => s.ViewCount)
            .Select(s => s.ToSnippetDto())
            .ToListAsync();

        return Result<List<SnippetDto>>.Success(snippets);
    }

    public async Task<Result> GetSnippetsByTag(string tagId)
    {
        var snippets = await snippetRepository.GetAllSnippets()
            .Where(s => !s.IsPrivate && s.Tags.Any(t => t.Id == tagId))
            .OrderByDescending(s => s.ViewCount)
            .ToListAsync();

        var snippetDtos = snippets.Select(s => s.ToSnippetDto())
            .ToList();

        return Result<List<SnippetDto>>.Success(snippetDtos);
    }

    public async Task<Result> GetSnippetById(string snippetId)
    {
        var snippet = await snippetRepository.GetAllSnippets()
            .FirstOrDefaultAsync(s => s.Id == snippetId);

        if (snippet is null)
        {
            return Result.Failure(SnippetErrors.SnippetNotFound);
        }

        if (snippet.IsPrivate)
        {
            var userId = userHelper.GetUserId();

            if (snippet.UserId != userId)
            {
                return Result.Failure(SnippetErrors.ViewRestricted);
            }
        }

        snippet.ViewCount++;
        await snippetRepository.UpdateAsync(snippet);

        return Result<SnippetDto>.Success(snippet.ToSnippetDto());
    }

    private static IQueryable<Snippet> ApplyFilter(
        IQueryable<Snippet> snippets,
        SnippetsFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Title))
        {
            snippets = snippets.Where(s => s.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrEmpty(filter.Language))
        {
            snippets = snippets.Where(s => s.Language.Equals(filter.Language));
        }

        if (filter.StartDate.HasValue)
        {
            snippets = snippets.Where(s => s.CreatedAt >= filter.StartDate.Value);
        }

        if (filter.EndDate.HasValue)
        {
            snippets = snippets.Where(s => s.CreatedAt <= filter.EndDate.Value);
        }

        return snippets;
    }
}
