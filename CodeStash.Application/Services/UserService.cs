using CodeStash.Application.Errors;
using CodeStash.Application.Interfaces;
using CodeStash.Application.Mappings;
using CodeStash.Application.Models;
using CodeStash.Application.Utilities;
using CodeStash.Application.Utilities.Pagination;
using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CodeStash.Application.Services;
public class UserService(UserManager<ApplicationUser> userManager,
                         SignInManager<ApplicationUser> signInManager,
                         IEmailService emailService,
                         UserHelper userHelper,
                         IPagedResultService pagedResultService,
                         IConfiguration config,
                         LinkGenerator linkGenerator,
                         IHttpContextAccessor httpContextAccessor,
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

    public async Task<Result> GetUserProfileAsync()
    {
        var userId = userHelper.GetUserId();
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        var profile = user.ToUserProfile();

        return Result<UserProfileDto>.Success(profile);
    }

    public async Task<Result> GetUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        var profile = user.ToUserDto();

        return Result<UserDto>.Success(profile);
    }

    public async Task<Result> GetUsersAsync(int pageNumber, int pageSize)
    {
        var users = userManager.Users;
        var ordered = users.OrderByDescending(a => a.CreatedAt);
        var userDtos = ordered.Select(a => a.ToUserDto());
        var paged = await pagedResultService
            .GetPagedResultAsync(userDtos, pageNumber, pageSize);

        return Result<PagedResult<UserDto>>.Success(paged);
    }

    public async Task<Result> InitiateEmailChangeAsync(EmailDto request)
    {
        var userId = userHelper.GetUserId();

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        var token = await userManager.GenerateChangeEmailTokenAsync(user, request.Email);
        var link = UrlHelper.GenerateLink(
        user.Id, token, "ConfirmEmailUpdate", "Users", config, linkGenerator, httpContextAccessor);

        var emailBody = $@"
    <html>
    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
        <h1 style='color: #444;'>Email Update</h1>
        <p>We received a request to update the email for your CodeStash account. If this was you, click the button below and enter the new email address:</p>
        <p><a href='{link}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Update email</a></p>
        <br>
        <p>Happy stashing,</p>
        <p>The CodeStash Team</p>
    </body>
    </html>";

        BackgroundJob.Enqueue(() => emailService.SendEmailAsync(
            user.UserName ?? string.Empty,
            request.Email,
            "CodeStash Email Update Request",
            emailBody));

        return Result.Success();
    }

    public async Task<Result> ConfirmEmailUpdateAsync(UpdateEmailModel request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        var decodedToken = Encoding.UTF8.GetString(
            WebEncoders.Base64UrlDecode(request.Token));

        var result = await userManager.ChangeEmailAsync(
            user, request.NewEmail, decodedToken);

        if (!result.Succeeded)
        {
            logger.LogError("Email update confirmation failed. User ID: {Id} Errors: {@Errors}",
            user.Id, result.Errors);
            return Result.Failure(UserErrors.EmailUpdateFailed);
        }

        await signInManager.SignOutAsync();
        return Result.Success();
    }

    public async Task<Result> UpdatePasswordAsync(UpdatePasswordModel request)
    {
        var userId = userHelper.GetUserId();

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        if (request.CurrentPassword == request.NewPassword)
        {
            return Result.Failure(UserErrors.SamePassword);
        }

        var result = await userManager.ChangePasswordAsync(
            user, request.CurrentPassword, request.ConfirmPassword);

        if (!result.Succeeded)
        {
            logger.LogError("Failed to update user password. User ID: {Id} Errors: {@Errors}",
            user.Id, result.Errors);
            return Result.Failure(UserErrors.PasswordUpdateFailed);
        }

        return Result.Success();
    }
}
