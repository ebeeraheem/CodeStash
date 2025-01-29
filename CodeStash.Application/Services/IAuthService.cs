using CodeStash.Application.Models;
using CodeStash.Core.Dtos;
using CodeStash.Core.Models;
using CodeStash.Core.Entities;

namespace CodeStash.Application.Services;
public interface IAuthService
{
    Task<Result<ApplicationUser>> AuthenticateUserAsync(LoginModel request);
    Task<Result> ConfirmEmailAsync(string userId, string token);
    Task<Result> ForgotPasswordAsync(EmailDto request);
    Task<Result> RegisterAsync(RegisterModel request);
    Task<Result> ResetPasswordAsync(ResetPasswordModel request);
    Task<Result> SendEmailConfirmationLink();
}