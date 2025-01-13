using Microsoft.AspNetCore.Identity;
using CodeStash.Application.Models;
using CodeStash.Application.Errors;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeStash.Core.Entities;
using Hangfire;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using CodeStash.Application.Interfaces;
using CodeStash.Application.Utilities;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public partial class AuthService(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IEmailService emailService,
                                 ITransactionManager transactionManager,
                                 IConfiguration config,
                                 UserHelper userHelper,
                                 LinkGenerator linkGenerator,
                                 IHttpContextAccessor httpContextAccessor,
                                 ILogger<AuthService> logger) : IAuthService
{
    public async Task<Result> RegisterAsync(RegisterModel request)
    {
        var isValidUserName = ValidateUserName(request.UserName);

        if (!isValidUserName)
        {
            return Result.Failure(AuthErrors.InvalidUserName);
        }

        var isUserNameTaken = await userManager.Users
            .AnyAsync(a => a.UserName != null &&
            a.UserName.Equals(request.UserName));

        if (isUserNameTaken)
        {
            return Result.Failure(AuthErrors.UserNameExist);
        }

        var user = new ApplicationUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            Role = Roles.User,
            LastLoginDate = DateTime.UtcNow
        };

        return await transactionManager.ExecuteInTransactionAsync(async () =>
        {
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                logger.LogError("User creation failed. Errors: {@Errors}", result.Errors);
                return Result.Failure(AuthErrors.RegistrationFailed);
            }

            var roleResult = await userManager.AddToRoleAsync(user, Roles.User);

            if (!roleResult.Succeeded)
            {
                logger.LogError("Failed to add user to role: {Role} Errors: {@Errors}",
                    Roles.User, roleResult.Errors);
                return Result.Failure(AuthErrors.CannotAddToRole);
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = UrlHelper.GenerateLink(
                user.Id, token, "ConfirmEmail", "Auth", config, linkGenerator, httpContextAccessor);

            var emailBody = $@"
    <html>
    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
        <h1 style='color: #444;'>Welcome to CodeStash!</h1>
        <p>Please verify your email:</p>
        <p><a href='{link}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Confirm your email</a></p>
        <br>
        <p>Happy stashing,</p>
        <p>The CodeStash Team</p>
    </body>
    </html>";

            BackgroundJob.Enqueue(() => emailService.SendEmailAsync(
                request.UserName,
                request.Email,
                "Welcome to CodeStash: Please confirm your email address",
                emailBody));

            await signInManager.SignInAsync(user, isPersistent: false);
            return Result.Success();
        });
    }

    public async Task<Result<ApplicationUser>> AuthenticateUserAsync(LoginModel request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result<ApplicationUser>.Failure(AuthErrors.UserNotFound);
        }

        var result = await signInManager
            .CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Result<ApplicationUser>.Failure(AuthErrors.LoginFailed);
        }

        return Result<ApplicationUser>.Success(user);
    }

    //public async Task<Result> LoginAsync(LoginModel request)
    //{
    //    var user = await userManager.FindByEmailAsync(request.Email);

    //    if (user is null)
    //    {
    //        return Result.Failure(AuthErrors.UserNotFound);
    //    }

    //    var result = await signInManager
    //        .CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

    //    if (!result.Succeeded)
    //    {
    //        return Result.Failure(AuthErrors.LoginFailed);
    //    }

    //    var claims = new List<Claim>
    //    {
    //        new(ClaimTypes.NameIdentifier, user.Id),
    //        new(ClaimTypes.Name, user.Email ?? string.Empty),
    //        new(ClaimTypes.Role, user.Role),
    //    };

    //    await userManager.AddClaimsAsync(user, claims);
    //    await signInManager.SignInWithClaimsAsync(user, isPersistent: true, claims);

    //    signInManager.Context.Response.Cookies.Append(".AspNetCore.Cookies", string.Empty, new CookieOptions
    //    {
    //        Secure = true,
    //        HttpOnly = true,
    //        IsEssential = true,
    //        SameSite = SameSiteMode.Strict,
    //        Expires = DateTime.UtcNow.AddDays(14)
    //    });

    //    user.LastLoginDate = DateTime.UtcNow;
    //    await userManager.UpdateAsync(user);
    //    return Result.Success();
    //}

    //public async Task<Result> LogoutAsync()
    //{
    //    await signInManager.SignOutAsync();
    //    signInManager.Context.Response.Cookies.Delete(".AspNetCore.Cookies");
    //    return Result.Success();
    //}

    public async Task<Result> ForgotPasswordAsync(EmailDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var link = UrlHelper.GenerateLink(
            user.Id, token, "ResetPassword", "Auth", config, linkGenerator, httpContextAccessor);

        var emailBody = $@"
    <html>
    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
        <h1 style='color: #444;'>Password Reset Request</h1>
        <p>We received a request to reset the password for your CodeStash account. If this was you, click the button below to create a new password:</p>
        <p><a href='{link}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Reset your password</a></p>
        <br>
        <p>If you didn’t request a password reset, you can safely ignore this email. Your account will remain secure.</p>
        <br>
        <p>Happy stashing,</p>
        <p>The CodeStash Team</p>
    </body>
    </html>";

        BackgroundJob.Enqueue(() => emailService.SendEmailAsync(
            user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            "Password Reset Request",
            emailBody));

        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordModel request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        var decodedToken = Encoding.UTF8.GetString(
            WebEncoders.Base64UrlDecode(request.Token));

        var result = await userManager.ResetPasswordAsync(
            user, decodedToken, request.ConfirmPassword);

        if (!result.Succeeded)
        {
            logger.LogError("Failed to reset user password. User ID: {Id} Errors: {@Errors}",
                user.Id, result.Errors);
            return Result.Failure(AuthErrors.PasswordResetFailed);
        }

        return Result.Success();
    }

    public async Task<Result> SendEmailConfirmationLink()
    {
        var userId = userHelper.GetUserId();
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        if (user.EmailConfirmed)
        {
            return Result.Failure(AuthErrors.EmailAlreadyConfirmed);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = UrlHelper.GenerateLink(
            user.Id, token, "ConfirmEmail", "Auth", config, linkGenerator, httpContextAccessor);

        var emailBody = $@"
    <html>
    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
        <h1 style='color: #444;'>Verify your email</h1>
        <p>Please verify your email:</p>
        <p><a href='{link}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Confirm your email</a></p>
        <br>
        <p>Happy stashing,</p>
        <p>The CodeStash Team</p>
    </body>
    </html>";

        BackgroundJob.Enqueue(() => emailService.SendEmailAsync(
            user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            "CodeStash Email Verification: Please confirm your email address",
            emailBody));

        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(string userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Failure(AuthErrors.UserNotFound);
        }

        if (user.EmailConfirmed)
        {
            return Result.Failure(AuthErrors.EmailAlreadyConfirmed);
        }

        var decodedToken = Encoding.UTF8.GetString(
            WebEncoders.Base64UrlDecode(token));

        var result = await userManager.ConfirmEmailAsync(user, decodedToken);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        logger.LogError("Email confirmation failed. User ID: {Id} Errors: {@Errors}",
            userId, result.Errors);
        return Result.Failure(AuthErrors.EmailConfirmationFailed);
    }

    private bool ValidateUserName(string userName)
    {
        var regex = AlphanumericUnderscoreRegex();
        return regex.IsMatch(userName);
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
    private static partial Regex AlphanumericUnderscoreRegex();
}
