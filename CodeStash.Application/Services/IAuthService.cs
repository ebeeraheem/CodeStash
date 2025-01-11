using CodeStash.Application.Models;

namespace CodeStash.Application.Services;
public interface IAuthService
{
    Task<Result> ConfirmEmailAsync(string userId, string token);
    Task<Result> LoginAsync(LoginModel request);
    Task<Result> LogoutAsync();
    Task<Result> RegisterAsync(RegisterModel request);
}