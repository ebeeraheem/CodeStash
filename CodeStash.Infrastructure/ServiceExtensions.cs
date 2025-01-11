﻿using CodeStash.Application.Interfaces;
using CodeStash.Application.Services;
using CodeStash.Core.Interfaces;
using CodeStash.Infrastructure.Audit;
using CodeStash.Infrastructure.EmailModule;
using CodeStash.Infrastructure.Persistence;
using Hangfire;
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

        services.AddHangfire(x => x.UseSqlServerStorage(
            configuration.GetConnectionString("Default")));
        services.AddHangfireServer();

        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.Configure<EmailSettings>(configuration.GetSection("SmtpSettings"));
        services.AddTransient<IEmailService, EmailService>();

        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<ITransactionManager, TransactionManager>();

        return services;
    }
}
