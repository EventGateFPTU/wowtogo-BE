using System.Linq.Expressions;
using Domain.Interfaces.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;
public class RepositoryBase<T>(WowToGoDBContext dbContext) : IRepositoryBase<T> where T : class
{
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();
    public void Add(T entity)
        => _dbSet.Add(entity);

    public void AddRange(IEnumerable<T> entities)
        => _dbSet.AddRange(entities);

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, CancellationToken cancellationToken = default)
        => trackChanges
            ? await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken)
            : await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, CancellationToken cancellationToken = default)
        => trackChanges
            ? await _dbSet.Where(predicate).ToListAsync(cancellationToken)
            : await _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> GetAsync(bool trackChanges = false, CancellationToken cancellationToken = default)
        => trackChanges
            ? await _dbSet.ToListAsync(cancellationToken)
            : await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> GetAsync(int page, int pageSize, bool trackChanges = false, CancellationToken cancellationToken = default)
        => trackChanges
            ? await _dbSet.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken)
            : await _dbSet.AsNoTracking().Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public void Remove(T entity)
        => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities)
        => _dbSet.RemoveRange(entities);
}