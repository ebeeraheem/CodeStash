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
public class AuthController(IAuthService authService) : ControllerBase
{
    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel request)
    {
        var result = await authService.RegisterAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel request)
    {
        //var result = await authService.AuthenticateUserAsync(request);

        //if (!result.IsSuccess)
        //{
        //    return Unauthorized(result);
        //}

        //var user = result.Data;

        //var claims = new List<Claim>
        //{
        //    new(ClaimTypes.NameIdentifier, user.Id),
        //    new(ClaimTypes.Name, user.Email ?? string.Empty),
        //    new(ClaimTypes.Role, user.Role),
        //};

        //var claimsIdentity = new ClaimsIdentity(
        //    claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //var authProperties = new AuthenticationProperties
        //{
        //    IsPersistent = true,
        //};

        //await HttpContext.SignInAsync(
        //    CookieAuthenticationDefaults.AuthenticationScheme,
        //    new ClaimsPrincipal(claimsIdentity),
        //    authProperties);

        //return Ok();

        var result = await authService.LoginAsync(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        //await HttpContext.SignOutAsync(
        //CookieAuthenticationDefaults.AuthenticationScheme);
        //return Ok();

        var result = await authService.LogoutAsync();
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(EmailDto request)
    {
        var result = await authService.ForgotPasswordAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel request)
    {
        var result = await authService.ResetPasswordAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [HttpPost("email-confirmation")]
    public async Task<IActionResult> SendConfirmationEmail()
    {
        var result = await authService.SendEmailConfirmationLink();

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [EnableRateLimiting(RateLimitModel.FixedLimit)]
    [AllowAnonymous]
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var result = await authService.ConfirmEmailAsync(userId, token);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
