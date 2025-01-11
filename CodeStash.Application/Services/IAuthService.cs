using CodeStash.Application.Models;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public interface IAuthService
{
    Task<Result> ConfirmEmailAsync(string userId, string token);
    Task<Result> ForgotPasswordAsync(EmailDto request);
    Task<Result> LoginAsync(LoginModel request);
    Task<Result> LogoutAsync();
    Task<Result> RegisterAsync(RegisterModel request);
    Task<Result> ResetPasswordAsync(ResetPasswordModel request);
    Task<Result> SendEmailConfirmationLink();
}