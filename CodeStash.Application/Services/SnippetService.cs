using CodeStash.Application.Errors;
using CodeStash.Application.Models;
using CodeStash.Application.Repositories;
using CodeStash.Application.Utilities;
using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;
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
            return Result.Failure(SnippetErrors.MaximumExceeded);
        }

        var userId = userHelper.GetUserId();
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
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
}
