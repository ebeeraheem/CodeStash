using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeStash.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext(options)
{
}
