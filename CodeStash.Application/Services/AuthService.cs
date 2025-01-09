using Microsoft.AspNetCore.Identity;
using CodeStash.Application.Models;
using CodeStash.Application.Errors;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeStash.Core.Entities;
using System.Security.Claims;

namespace CodeStash.Application.Services;
public partial class AuthService(UserManager<ApplicationUser> userManager,
                         SignInManager<ApplicationUser> signInManager,
                         ILogger<AuthService> logger) : IAuthService
{
    public async Task<Result> LoginAsync(LoginModel request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        var result = await signInManager
            .CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Result.Failure(AuthErrors.LoginFailed);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Email ?? string.Empty),
            new(ClaimTypes.Role, user.Role),
        };

        var claimsIdentity = new ClaimsIdentity(claims);

        await userManager.AddClaimsAsync(user, claims);
        await signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);

        user.LastLoginDate = DateTime.UtcNow;
        await userManager.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> LogoutAsync()
    {
        await signInManager.SignOutAsync();
        return Result.Success();
    }

    public async Task<Result> RegisterAsync(RegisterModel request)
    {
        var isValidUserName = ValidateUserName(request.UserName);

        if (!isValidUserName)
        {
            return Result.Failure(AuthErrors.InvalidUserName);
        }

        var isUserNameAvailable = await userManager.Users
            .AnyAsync(a => a.UserName != null &&
            a.UserName.Equals(request.UserName));

        if (!isUserNameAvailable)
        {
            return Result.Failure(AuthErrors.UserNameExist);
        }

        var user = new ApplicationUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            LastLoginDate = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            logger.LogError("User creation failed. Errors: {@Errors}", result.Errors);
            return Result.Failure(AuthErrors.RegistrationFailed);
        }

        // Add user to default user role

        await signInManager.SignInAsync(user, isPersistent: false);
        return Result.Success();
    }

    private bool ValidateUserName(string userName)
    {
        var regex = AlphanumericUnderscoreRegex();
        return regex.IsMatch(userName);
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
    private static partial Regex AlphanumericUnderscoreRegex();
}
