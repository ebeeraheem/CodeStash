using CodeStash.Application.Interfaces;
using CodeStash.Application.Repositories;
using CodeStash.Core.Entities;
using CodeStash.Core.Interfaces;
using CodeStash.Infrastructure.Audit;
using CodeStash.Infrastructure.EmailModule;
using CodeStash.Infrastructure.Persistence;
using CodeStash.Infrastructure.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeStash.Infrastructure;
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
        })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddHangfire(x => x.UseSqlServerStorage(
            configuration.GetConnectionString("Default")));
        services.AddHangfireServer();

        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.AddScoped<ISnippetRepository, SnippetRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.Configure<EmailSettings>(configuration.GetSection("SmtpSettings"));
        services.AddTransient<IEmailService, EmailService>();

        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<ITransactionManager, TransactionManager>();

        return services;
    }
}
