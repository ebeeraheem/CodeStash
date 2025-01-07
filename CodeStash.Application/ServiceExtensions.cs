using CodeStash.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeStash.Application;
public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
