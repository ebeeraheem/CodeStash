using Microsoft.AspNetCore.Identity;
using CodeStash.Core;
using CodeStash.Application.Models;
using CodeStash.Application.Errors;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

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

        var result = await signInManager.PasswordSignInAsync(
            user, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            user.LastLoginDate = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            return Result.Success();
        }

        return Result.Failure(AuthErrors.LoginFailed);
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
        await userManager.AddToRoleAsync(user, Roles.User);

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
