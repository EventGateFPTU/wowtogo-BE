using System.Linq.Expressions;

namespace Domain.Interfaces.Data.IRepositories;
public interface IRepositoryBase<T> where T : class
{
    // DBSet
    IQueryable<T> DBSet();
    // Get
    Task<IEnumerable<T>> GetAsync(bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAsync(int page, int pageSize, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, CancellationToken cancellationToken = default);
    // Add
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    // Remove
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}