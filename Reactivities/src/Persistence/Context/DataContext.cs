using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;
public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; }
}
