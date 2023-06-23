using System.Data;
using BuildingBlocks.Commons.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BuildingBlocks.Dapper;
public class PgsqlDbContext : DapperDbContext, IPgsqlDbContext
{
    public IDbConnection _pgConnection { get; } 
    public PgsqlDbContext(IConfiguration config, IDbConnection connection)
        : base(config, connection)
    {
        _pgConnection = connection;
    }

}