using System.Linq.Expressions;

namespace BuildingBlocks.Commons.Interfaces;

public interface IRepositoryBase<T>
{
    Task<T?> GetValue(Expression<Func<T, bool>> expression, bool AsNoTracking = true);
    Task<TResponse?> GetValue<TResponse>(Expression<Func<T, bool>> expression, Expression<Func<T, TResponse>> selector, bool AsNoTracking = true);
    Task<IEnumerable<T>> GetAllValues(bool AsNoTracking = true);
    Task<IEnumerable<T>> GetAllValues(Expression<Func<T, bool>>? expression = null, bool AsNoTracking = true);
    Task<IEnumerable<T>> GetAllValues(Expression<Func<T, bool>>? expression = null, List<Expression<Func<T, object>>>? includes = null, bool AsNoTracking = true);
    Task Create(T entity);
    void Delete(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}