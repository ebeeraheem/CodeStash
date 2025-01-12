﻿using CodeStash.Core.Entities;
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

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
        })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

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
            });

        services.AddAuthorization();

        // Suppress Redirects Globally for the API
        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });

        services.AddInfrastructureServices(configuration);

        return services;
    }

    public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
    {
        await serviceProvider.SeedRoles();
        await serviceProvider.SeedUsers();
    }
}
