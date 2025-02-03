using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CodeStash.Application.Utilities;
public static class UrlHelper
{
    public static string GenerateLink(
        string userId,
        string token,
        string action,
        string controller,
        IConfiguration config,
        LinkGenerator linkGenerator,
        IHttpContextAccessor httpContextAccessor)
    {
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var apiUrl = config["CodeStash:ApiUrl"];
        var httpContext = httpContextAccessor.HttpContext;

        ArgumentNullException.ThrowIfNull(apiUrl);
        ArgumentNullException.ThrowIfNull(httpContext);

        var path = linkGenerator.GetPathByAction(
            action,
            controller,
            values: new { userId, token = encodedToken });

        if (string.IsNullOrWhiteSpace(path))
            throw new InvalidOperationException("Failed to generate link");

        return $"{apiUrl}{path}";
    }
}
