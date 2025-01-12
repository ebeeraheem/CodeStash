using CodeStash.Application.Models;

namespace CodeStash.Application.Services;
public interface IUserService
{
    Task<Result> GetUserProfileAsync();
    Task<Result> UpdateProfileAsync(ProfileModel request);
}