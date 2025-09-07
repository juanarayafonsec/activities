using Activities.Application.Interfaces;
using Activities.Infrastructure.Persistance.Context;
using Activities.Infrastructure.Persistance.Respository;

namespace Activities.Infrastructure.Persistance;
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ActivityContext _dbContext;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(ActivityContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var existing))
        {
            return (IGenericRepository<T>)existing;
        }

        var created = new GenericRepository<T>(_dbContext);
        _repositories[typeof(T)] = created;
        return created;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var rows = await _dbContext.SaveChangesAsync(cancellationToken);
        return rows;
    }
}
