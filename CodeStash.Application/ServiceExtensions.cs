using CodeStash.Application.Services;
using CodeStash.Application.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeStash.Application;
public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<UserHelper>();

        return services;
    }
}
