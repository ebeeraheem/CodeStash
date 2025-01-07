using CodeStash.Application.Models;
using CodeStash.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeStash.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost("profile/update")]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileModel request)
    {
        var result = await userService.UpdateProfileAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
