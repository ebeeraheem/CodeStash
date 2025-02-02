using CodeStash.Application.Models;
using CodeStash.Application.Services;
using CodeStash.Core.Dtos;
using CodeStash.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CodeStash.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPut("profile/update")]
    public async Task<IActionResult> UpdateProfile(ProfileModel request)
    {
        var result = await userService.UpdateProfileAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var result = await userService.GetUserProfileAsync();

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var result = await userService.GetUserAsync(userId);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetUsers(int pageNumber = 1, int pageSize = 20)
    {
        var result = await userService.GetUsersAsync(pageNumber, pageSize);

        return Ok(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPut("email/update")]
    public async Task<IActionResult> UpdateEmail(EmailDto request)
    {
        var result = await userService.InitiateEmailChangeAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpPost("email/confirm-update")]
    public async Task<IActionResult> ConfirmEmailUpdate(UpdateEmailModel request)
    {
        var result = await userService.ConfirmEmailUpdateAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPut("password/update")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordModel request)
    {
        var result = await userService.UpdatePasswordAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
