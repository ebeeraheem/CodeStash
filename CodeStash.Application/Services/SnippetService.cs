using CodeStash.Application.Errors;
using CodeStash.Application.Models;
using CodeStash.Application.Repositories;
using CodeStash.Application.Utilities;
using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;
using CodeStash.Core.Models;
using Markdig;
using Microsoft.AspNetCore.Identity;

namespace CodeStash.Application.Services;
public class SnippetService(ISnippetRepository snippetRepository,
                            UserManager<ApplicationUser> userManager,
                            UserHelper userHelper) : ISnippetService
{
    public async Task<Result> AddSnippetAsync(SnippetDto request)
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
}
