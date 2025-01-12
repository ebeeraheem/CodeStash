using CodeStash.Core.Dtos;
using CodeStash.Core.Entities;

namespace CodeStash.Application.Mappings;
public static class UserMappings
{
    public static UserProfileDto ToUserProfile(this ApplicationUser user)
    {
        return new UserProfileDto()
        {
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
        };
    }
}
