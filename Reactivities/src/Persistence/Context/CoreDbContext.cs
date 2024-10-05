using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class CoreDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Activity> Activities { get; set; }
}