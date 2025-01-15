using CodeStash.Application.Filters;
using CodeStash.Application.Services;
using CodeStash.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeStash.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SnippetsController(ISnippetService snippetService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddSnippet(AddSnippetDto request)
    {
        var result = await snippetService.AddSnippetAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{snippetId}")]
    public async Task<IActionResult> UpdateSnippet(Guid snippetId, UpdateSnippetDto request)
    {
        var result = await snippetService.UpdateSnippetAsync(snippetId, request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{snippetId}")]
    public async Task<IActionResult> DeleteSnippet(Guid snippetId)
    {
        var result = await snippetService.DeleteSnippetAsync(snippetId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

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

    [AllowAnonymous]
    [HttpGet("languages")]
    public IActionResult GetSnippetLanguages()
    {
        var result = snippetService.GetAllSnippetLanguages();

        return Ok(result);
    }

    [HttpGet("personal-stash")]
    public async Task<IActionResult> GetMySnippets()
    {
        var result = await snippetService.GetMySnippets();

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet("authors/{userName}")]
    public async Task<IActionResult> GetAuthorSnippets(string userName)
    {
        var result = await snippetService.GetSnippetsByAuthorUserName(userName);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet("tags/{tagId}")]
    public async Task<IActionResult> GetSnippetsByTag(Guid tagId)
    {
        var result = await snippetService.GetSnippetsByTag(tagId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet("{snippetId}")]
    public async Task<IActionResult> GetSnippetById(Guid snippetId)
    {
        var result = await snippetService.GetSnippetById(snippetId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
