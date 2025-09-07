using Activities.Domain.Entity;
using Activities.Domain.Interfaces;
using Activities.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Activities.Infrastructure.Persistance.Respository;
public class ActivityRepository(ActivityContext context) : IActivityRepository
{
    public async Task<int> AddAsync(Activity activity, CancellationToken cancellationToken)
    {
        context.Activities.Add(activity);

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Activity activity, CancellationToken cancellationToken)
    {

        context.Activities.Remove(activity);

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Activity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Activities.FindAsync([id], cancellationToken);
    }

    public async Task<List<Activity>> ListAsync(CancellationToken cancellationToken)
    {
        return await context.Activities.ToListAsync(cancellationToken);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
