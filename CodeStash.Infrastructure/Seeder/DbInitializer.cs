using CodeStash.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeStash.Infrastructure.Seeder;
public static class DbInitializer
{
    public static async Task SeedRoles(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<ApplicationRole>>();

        if (roleManager.Roles.Any())
            return;

        List<string> roles = ["Admin", "User"];
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = role });
        }
    }

    public static async Task SeedUsers(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        if (userManager.Users.Any())
            return;

        var config = serviceProvider.GetRequiredService<IConfiguration>();

        var firstName = config["DefaultUser:FirstName"];
        var lastName = config["DefaultUser:LastName"];
        var email = config["DefaultUser:Email"];
        var userName = config["DefaultUser:UserName"];
        var password = config["DefaultUser:Password"];

        if (string.IsNullOrEmpty(firstName) ||
            string.IsNullOrEmpty(lastName) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(userName) ||
            string.IsNullOrEmpty(password))
        {
            return;
        }

        var admin = new ApplicationUser()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = userName,
            Role = "Admin"
        };

        var result = await userManager.CreateAsync(admin, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, admin.Role);
        }
    }
}
