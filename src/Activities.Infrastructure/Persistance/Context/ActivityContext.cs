using Activities.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Activities.Infrastructure.Persistance.Context;
public class ActivityContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Activity> Activities { get; set; }
}