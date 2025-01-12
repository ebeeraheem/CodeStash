using CodeStash.Application.Models;
using CodeStash.Application.Services;
using CodeStash.Core.Dtos;
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

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var result = await userService.GetUserProfileAsync();

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var result = await userService.GetUserAsync(userId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("email/update")]
    public async Task<IActionResult> UpdateEmail(EmailDto request)
    {
        var result = await userService.InitiateEmailChangeAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("email/confirm-update")]
    public async Task<IActionResult> ConfirmEmailUpdate(UpdateEmailModel request)
    {
        var result = await userService.ConfirmEmailUpdateAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
