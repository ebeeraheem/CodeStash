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

        return services;
    }
}
