using CodeStash.Application.Errors;
using CodeStash.Application.Models;
using CodeStash.Application.Utilities;
using CodeStash.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CodeStash.Application.Services;
public class UserService(UserManager<ApplicationUser> userManager,
                         UserHelper userHelper,
                         ILogger<UserService> logger) : IUserService
{
    public async Task<Result> UpdateProfileAsync(ProfileModel request)
    {
        var userId = userHelper.GetUserId();
        ArgumentNullException.ThrowIfNullOrEmpty(userId);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        var result = await userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        logger.LogError("Profile update failed. Errors: {@Errors}", result.Errors);
        return Result.Failure(UserErrors.ProfileUpdateFailed);
    }

    //public async Task<Result> GetUsersAsync()
    //{

    //}
}
