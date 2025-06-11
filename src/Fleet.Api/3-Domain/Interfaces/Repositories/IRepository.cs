using System.Linq.Expressions;

namespace Fleet.Api._3_Domain.Interfaces.Repositories;

/// <summary>
/// Generic interface for basic CRUD operations.
/// </summary>
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

    Task<T?> GetByIdAsync(Guid id);
    Task<TResult?> GetByIdAsync<TResult>(Guid id, Expression<Func<T, TResult>> selector);

    Task AddAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    void Update(T entity);
    void Remove(T entity);
    Task<int> SaveChangesAsync();
}
