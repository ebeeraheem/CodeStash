using CodeStash.Core.Entities;
using CodeStash.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeStash.Infrastructure.Seeder;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        try
        {
            await using var scope = serviceProvider.CreateAsyncScope();
            await MigrateDatabaseAsync(scope.ServiceProvider);
            await SeedRolesAsync(scope.ServiceProvider);
            await SeedUsersAsync(scope.ServiceProvider);
            await SeedTagsAsync(scope.ServiceProvider);
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<object>>();
            logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }

    private static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }

    private static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider
            .GetRequiredService<RoleManager<ApplicationRole>>();

        if (await roleManager.Roles.AnyAsync())
            return;

        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            var result = await roleManager
                .CreateAsync(new ApplicationRole { Name = role });

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Default role '{role}' cannot be created: {string.Join(", ", result.Errors)}");
            }
        }
    }

    private static async Task SeedUsersAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        if (await userManager.Users.AnyAsync())
            return;

        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var defaultUser = config.GetSection("DefaultUser")
            .Get<DefaultUserConfig>();

        if (defaultUser?.IsValid() != true)
        {
            throw new InvalidOperationException(
                "Default user configuration is missing or invalid");
        }

        var admin = new ApplicationUser
        {
            FirstName = defaultUser.FirstName,
            LastName = defaultUser.LastName,
            Email = defaultUser.Email,
            UserName = defaultUser.UserName,
            Role = "Admin",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, defaultUser.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Default admin user cannot be created: {string.Join(", ", result.Errors)}");
        }

        result = await userManager.AddToRoleAsync(admin, admin.Role);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Cannot add user to role: {string.Join(", ", result.Errors)}");
        }
    }

    private static async Task SeedTagsAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        if (await context.Tags.AnyAsync())
            return;

        await context.Tags.AddRangeAsync(SeedTags.GetInitialTags());
        await context.SaveChangesAsync();
    }
}
