using System.Linq.Expressions;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.EFCore;

public abstract class RepositoryBase<T> : IReadRepository<T>, IWriteRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContextBase _context;
    protected RepositoryBase(ApplicationDbContextBase context)
    {
        _context = context; 
    }

    public async Task Add(T entity, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRange(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
    }

    public async Task<T?> GetValue(Expression<Func<T, bool>> expression, bool AsNoTracking = true)
    {
        
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(expression);

    }

    public async Task<TResponse?> GetValue<TResponse>(Expression<Func<T, bool>> expression, Expression<Func<T, TResponse>> selector, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        return await query.Where(expression).Select(selector).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllValues(bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();
        return await query.ToListAsync(); 
    }

    public async Task<IEnumerable<T>> GetAllValuesByExp(Expression<Func<T, bool>> expression, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        return await query.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllValuesByExp(Expression<Func<T, bool>> expression, List<Expression<Func<T, object>>>? includes = null, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();
        
        if(includes is not null)
            query = includes.Aggregate(query, (context, include) => context.Include(include));

        return await query.Where(expression).ToListAsync();
    }
}