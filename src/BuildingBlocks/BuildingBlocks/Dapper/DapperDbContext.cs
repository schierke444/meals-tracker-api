using System.Data;
using BuildingBlocks.Commons.Interfaces;
using Dapper;
using DapperQueryBuilder;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.Dapper;

public abstract class DapperDbContext: IReadDbContext, IDisposable
{
    protected readonly IDbConnection connection;

    public DapperDbContext (IConfiguration config, IDbConnection connection)
    {
        this.connection = connection; 
    }

    public void Dispose()
    {
        connection.Dispose(); 
    }

    public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await connection.QueryAsync<T>(sql, param, transaction);
    }

    public virtual async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public virtual async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, TResult>(string sql, Func<T1, T2, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
    {
        return await connection.QueryAsync(sql, map, param, transaction, true, splitOn);
    }

    public virtual async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, T3, TResult>(string sql, Func<T1, T2, T3, TResult> map, object? param = null, IDbTransaction? transaction = null, string splitOn = "Id", CancellationToken cancellationToken = default)
    {
        return await connection.QueryAsync(sql, map, param, transaction, true, splitOn);
    }

    public virtual async Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction);
    }

    public async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        return await connection.ExecuteScalarAsync<T>(sql, param, transaction);
    }
}