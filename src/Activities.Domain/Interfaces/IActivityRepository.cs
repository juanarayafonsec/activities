using Activities.Domain.Entity;

namespace Activities.Domain.Interfaces;
public interface IActivityRepository
{
    Task<Activity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Activity>> ListAsync(CancellationToken cancellationToken);
    Task<int> AddAsync(Activity activity, CancellationToken cancellationToken); 
    Task<int> SaveAsync(CancellationToken cancellationToken);
    Task<int> DeleteAsync(Activity activity, CancellationToken cancellationToken);
}
