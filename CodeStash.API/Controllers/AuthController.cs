using CodeStash.Application.Models;
using CodeStash.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeStash.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel request)
    {
        var result = await authService.LoginAsync(request);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
