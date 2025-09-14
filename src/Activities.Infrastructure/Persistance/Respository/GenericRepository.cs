using Activities.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Activities.Infrastructure.Persistance.Respository;

public sealed class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _set;

    public GenericRepository(DbContext dbContext)
    {
        _set = dbContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return await _set.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>> include, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _set.AsNoTracking();

        if (include is not null)
        {
            query = include(query);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _set;

        if (include is not null)
        {
            query = include(query);
        }

        var entity = await query.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        return entity;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _set.AddRangeAsync(entities, cancellationToken);
    }

    public void Update(T entity)
    {
        _set.Update(entity);
    }

    public void Remove(T entity)
    {
        _set.Remove(entity);
    }
}