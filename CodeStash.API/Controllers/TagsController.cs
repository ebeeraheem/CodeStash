using CodeStash.Application.Models;
using CodeStash.Application.Services;
using CodeStash.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeStash.API.Controllers;

[Authorize(Roles = Roles.Admin)]
[Route("api/[controller]")]
[ApiController]
public class TagsController(ITagService tagService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddTag(TagModel request)
    {
        var result = await tagService.AddTagAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(Guid id, TagModel request)
    {
        var result = await tagService.UpdateTagAsync(id, request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(Guid id)
    {
        var result = await tagService.DeleteTagAsync(id);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var result = await tagService.GetAllTags();

        return Ok(result);
    }
}
