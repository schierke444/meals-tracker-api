using BuildingBlocks.Commons.Models;

namespace BuildingBlocks.Commons.Interfaces;

public interface IWriteRepository<T> where T : BaseEntity
{
    Task Add(T entity, CancellationToken cancellationToken = default);
    Task AddRange(ICollection<T> entities, CancellationToken cancellationToken = default);
    void Delete(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);   
}