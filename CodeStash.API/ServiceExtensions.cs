using CodeStash.Core;
using CodeStash.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace CodeStash.API;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
                                                    ConfigureHostBuilder host)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

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
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
