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
}
