using CodeStash.Core.Entities;
using CodeStash.Infrastructure.Persistence;
using CodeStash.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Serilog;
using CodeStash.Infrastructure.Seeder;

namespace CodeStash.API;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
                                                    IConfiguration configuration,
                                                    ConfigureHostBuilder host)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        host.UseSerilog((context, configuration) =>
        {
            configuration.WriteTo.Console();
            configuration.Enrich.FromLogContext();
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        services.AddInfrastructureServices(configuration);

        // Configure cookie authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
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
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
    {
        await serviceProvider.SeedRoles();
        await serviceProvider.SeedUsers();
    }
}
