using CodeStash.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Threading.RateLimiting;
using CodeStash.Application.Models;

namespace CodeStash.API;

public static class ApiExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
                                                    IConfiguration configuration,
                                                    ConfigureHostBuilder host)
    {
        var UIUrl = configuration["CodeStash:UIUrl"];
        ArgumentNullException.ThrowIfNull(UIUrl, nameof(UIUrl));

        services.AddCors(options =>
        {
            options.AddPolicy("UIPolicy",
                policy =>
                policy
                    .WithOrigins(UIUrl)
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
}
