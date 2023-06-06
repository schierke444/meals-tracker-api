using System.Linq.Expressions;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.EFCore;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    protected readonly ApplicationDbContextBase _context;
    protected RepositoryBase(ApplicationDbContextBase context)
    {
        _context = context; 
    }


    public async Task<T?> GetValue(Expression<Func<T, bool>> expression, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task<TResponse?> GetValue<TResponse>(Expression<Func<T, bool>> expression, Expression<Func<T, TResponse>> selector , bool AsNoTracking = true)
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

    public async Task<IEnumerable<T>> GetAllValues(Expression<Func<T, bool>>? expression = null, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        if(expression != null)
            query = query.Where(expression);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TResponse>> GetAllValues<TResponse>(Expression<Func<T, TResponse>> selector, Expression<Func<T, bool>>? expression = null, bool AsNoTracking = true)
    { 
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        if(expression != null)
            query = query.Where(expression);

        return await query.Select(selector).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllValues(Expression<Func<T, bool>>? expression = null, List<Expression<Func<T, object>>>? includes = null, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        if(expression != null)
            query = query.Where(expression);

        if(includes != null)
            query = includes.Aggregate(query, (context, include) => context.Include(include));

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TResponse>> GetAllValues<TResponse>(Expression<Func<T, TResponse>> selector, Expression<Func<T, bool>>? expression = null, List<Expression<Func<T, object>>>? includes = null,
        bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();

        if(expression != null)
            query = query.Where(expression);

        if(includes != null)
            query = includes.Aggregate(query, (context, include) => context.Include(include));

        return await query.Select(selector).ToListAsync();

    }

    public async Task Create(T entity)
    {
        await _context.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}