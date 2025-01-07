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
