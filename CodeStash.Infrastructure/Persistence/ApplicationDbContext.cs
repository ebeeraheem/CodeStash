using CodeStash.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeStash.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext(options)
{
    public required DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public required DbSet<ApplicationRole> ApplicationRoles { get; set; }
}
