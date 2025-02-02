using CodeStash.Application.Filters;
using CodeStash.Application.Models;
using CodeStash.Application.Services;
using CodeStash.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CodeStash.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class SnippetsController(ISnippetService snippetService) : ControllerBase
{
    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPost]
    public async Task<IActionResult> AddSnippet(AddSnippetDto request)
    {
        var result = await snippetService.AddSnippetAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPut("{snippetId}")]
    public async Task<IActionResult> UpdateSnippet(string snippetId, UpdateSnippetDto request)
    {
        var result = await snippetService.UpdateSnippetAsync(snippetId, request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{snippetId}")]
    public async Task<IActionResult> DeleteSnippet(string snippetId)
    {
        var result = await snippetService.DeleteSnippetAsync(snippetId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllSnippets(
        [FromQuery] SnippetsFilter filter,
        int pageNumber = 1,
        int pageSize = 20)
    {
        var result = await snippetService.GetSnippets(pageNumber, pageSize, filter);

        return Ok(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpGet("languages")]
    public IActionResult GetSnippetLanguages()
    {
        var result = snippetService.GetAllSnippetLanguages();

        return Ok(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpGet("personal-stash")]
    public async Task<IActionResult> GetMySnippets()
    {
        var result = await snippetService.GetMySnippets();

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpGet("authors/{authorId}")]
    public async Task<IActionResult> GetAuthorSnippets(string authorId)
    {
        var result = await snippetService.GetSnippetsByAuthorId(authorId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpGet("tags/{tagId}")]
    public async Task<IActionResult> GetSnippetsByTag(string tagId)
    {
        var result = await snippetService.GetSnippetsByTag(tagId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpGet("{snippetId}")]
    public async Task<IActionResult> GetSnippetById(string snippetId)
    {
        var result = await snippetService.GetSnippetById(snippetId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
