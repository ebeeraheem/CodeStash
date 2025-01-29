using CodeStash.Core.Entities;
using CodeStash.Infrastructure.Persistence;
using CodeStash.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Serilog;
using CodeStash.Infrastructure.Seeder;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.RateLimiting;
using CodeStash.Application.Models;

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

        var wasmUiUrl = configuration["CodeStash:WasmUIUrl"];
        ArgumentNullException.ThrowIfNull(wasmUiUrl, nameof(wasmUiUrl));

        services.AddCors(options =>
        {
            options.AddPolicy("WasmUIPolicy",
                policy =>
                policy
                    .WithOrigins(wasmUiUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        services.AddSwaggerGen(options =>
        {
            // Add the API info and description
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "CodeStash API",
                Version = "v1",
                Description = "A secure and organized place to stash, manage, and share your code snippets with ease.",
                Contact = new OpenApiContact()
                {
                    Name = "Ibrahim Suleiman",
                    Url = new Uri("https://ebeesule.netlify.app")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter your token here:",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
               {
                   new OpenApiSecurityScheme
                   {
                       Reference = new OpenApiReference
                       {
                           Type = ReferenceType.SecurityScheme,
                           Id = "Bearer",
                       }
                   },

                   Array.Empty<string>()
               }
            });

            // Add XML comments in SwaggerUI
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
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
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;

                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();

        var rateLimitOptions = new RateLimitModel();
        configuration.GetSection(RateLimitModel.FixedLimit)
            .Bind(rateLimitOptions);

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy("fixed", httpContext => 
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = rateLimitOptions.PermitLimit,
                        Window = TimeSpan.FromSeconds(rateLimitOptions.Window)
                    }));
        });

        services.AddInfrastructureServices(configuration);

        return services;
    }

    public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
    {
        await DbInitializer.InitializeAsync(serviceProvider);
    }
}
