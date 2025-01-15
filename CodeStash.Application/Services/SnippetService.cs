using CodeStash.Application.Errors;
using CodeStash.Application.Filters;
using CodeStash.Application.Mappings;
using CodeStash.Application.Models;
using CodeStash.Application.Repositories;
using CodeStash.Application.Utilities;
using CodeStash.Application.Utilities.Pagination;
using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;
using CodeStash.Core.Models;
using Markdig;
using Microsoft.AspNetCore.Identity;

namespace CodeStash.Application.Services;
public class SnippetService(ISnippetRepository snippetRepository,
                            UserManager<ApplicationUser> userManager,
                            IPagedResultService pagedResultService,
                            UserHelper userHelper) : ISnippetService
{
    public async Task<Result> AddSnippetAsync(AddSnippetDto request)
    {
        const int MaxTags = 5;

        if (request.Tags.Count > MaxTags)
        {
            return Result.Failure(SnippetErrors.MaximumTagsExceeded);
        }

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

        var snippet = new Snippet()
        {
            Title = request.Title,
            Content = Markdown.ToHtml(request.Content),
            Language = request.Language,
            UserId = userId,
            User = user,
            Tags = request.Tags
        };

        var result = await snippetRepository.AddAsync(snippet);

        if (result <= 0)
        {
            return Result.Failure(SnippetErrors.CannotCreate);
        }

        return Result.Success();
    }

    public Result GetAllSnippetLanguages()
    {
        var languages = SnippetLanguage.GetAll();

        return Result<List<string?>>.Success(languages);
    }

    public async Task<Result> UpdateSnippetAsync(Guid snippetId, UpdateSnippetDto request)
    {
        const int MaxTags = 5;

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

        // Only update tags if flag is true
        if (request.UpdateTags)
        {
            if (request.Tags?.Count > MaxTags)
            {
                return Result.Failure(SnippetErrors.MaximumTagsExceeded);
            }

            snippet.Tags = request.Tags ?? [];
        }

        var result = await snippetRepository.UpdateAsync(snippet);

        if (result <= 0)
        {
            return Result.Failure(SnippetErrors.UpdateFailed);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteSnippetAsync(Guid snippetId)
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
        var snippets = snippetRepository.GetSnippetsWithAuthor();
        var publicSnippets = snippets.Where(s => !s.IsPrivate);
        var filtered = ApplyFilter(publicSnippets, filter);
        var ordered = filtered.OrderBy(s => s.ViewCount);
        var snippetDtos = ordered.Select(s => s.ToSnippetDto());

        var paginated = await pagedResultService
            .GetPagedResultAsync(snippetDtos, pageNumber, pageSize);

        return Result<PagedResult<SnippetDto>>.Success(paginated);
    }

    public async Task<Result> GetSnippetById(Guid snippetId)
    {
        var snippet = await snippetRepository.GetByIdAsync(snippetId);

        if (snippet is null)
        {
            return Result.Failure(SnippetErrors.SnippetNotFound);
        }

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
            snippets = snippets.Where(s => s.Language.Contains(filter.Language));
        }

        if (filter.Tags?.Count > 0)
        {
            snippets = snippets.Where(s => s.Tags.Any(tag => filter.Tags.Contains(tag)));
        }

        if (!string.IsNullOrEmpty(filter.AuthorUserName))
        {
            snippets = snippets.Where(s => s.User.UserName!= null &&
                s.User.UserName.Contains(filter.AuthorUserName));
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
