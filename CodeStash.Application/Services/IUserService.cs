using CodeStash.Application.Models;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public interface IUserService
{
    Task<Result> ConfirmEmailUpdateAsync(UpdateEmailModel request);
    Task<Result> GetUserAsync(string userId);
    Task<Result> GetUserProfileAsync();
    Task<Result> InitiateEmailChangeAsync(EmailDto request);
    Task<Result> UpdateProfileAsync(ProfileModel request);
}