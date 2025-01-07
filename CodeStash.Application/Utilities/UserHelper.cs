using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CodeStash.Application.Utilities;
public class UserHelper(IHttpContextAccessor httpContextAccessor)
{
    public string GetUserId()
    {
        return httpContextAccessor?.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            throw new InvalidOperationException("User is unauthenticated.");
    }
}
