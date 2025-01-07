using Microsoft.AspNetCore.Identity;
using CodeStash.Core;
using CodeStash.Application.Models;
using CodeStash.Application.Errors;

namespace CodeStash.Application.Services;
public class AuthService(UserManager<ApplicationUser> userManager,
                         SignInManager<ApplicationUser> signInManager) : IAuthService
{
    public async Task<Result> LoginAsync(LoginModel request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        var result = await signInManager.PasswordSignInAsync(
            request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            user.LastLoginDate = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            return Result.Success();
        }

        return Result.Failure(AuthErrors.LoginFailed);
    }
}
