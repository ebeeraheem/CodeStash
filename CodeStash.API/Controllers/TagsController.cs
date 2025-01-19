using CodeStash.Application.Models;
using CodeStash.Application.Services;
using CodeStash.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CodeStash.API.Controllers;

[Authorize(Roles = Roles.Admin)]
[Route("api/[controller]")]
[ApiController]
public class TagsController(ITagService tagService) : ControllerBase
{
    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPost]
    public async Task<IActionResult> AddTag(TagModel request)
    {
        var result = await tagService.AddTagAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(string id, TagModel request)
    {
        var result = await tagService.UpdateTagAsync(id, request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(string id)
    {
        var result = await tagService.DeleteTagAsync(id);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var result = await tagService.GetAllTags();

        return Ok(result);
    }
}
