using CodeStash.Core.Interfaces;
using CodeStash.Infrastructure.Audit;
using CodeStash.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeStash.Infrastructure;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddHttpContextAccessor();

        services.AddScoped<IAuditService, AuditService>();

        return services;
    }
}
