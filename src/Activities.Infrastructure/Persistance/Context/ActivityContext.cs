using Activities.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Activities.Infrastructure.Persistance.Context;
public class ActivityContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public required DbSet<Activity> Activities { get; set; }
}