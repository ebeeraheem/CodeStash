using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Scalar.AspNetCore;
using Serilog;

namespace CodeStash.API;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
                                                    ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
        {
            configuration.WriteTo.Console();
            configuration.Enrich.FromLogContext();
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        // Configure cookie authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.LoginPath = "/api/auth/login";
                options.LogoutPath = "/api/auth/logout";
                options.AccessDeniedPath = "/api/auth/access-denied";
            });

        services.AddAuthorization();

        return services;
    }

    public static void MapApiServices(this WebApplication app)
    {
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("CodeStash")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
    }
}
