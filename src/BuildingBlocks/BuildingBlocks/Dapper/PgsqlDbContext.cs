using System.Data;
using BuildingBlocks.Commons.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BuildingBlocks.Dapper;
public class PgsqlDbContext : DapperDbContext, IPgsqlDbContext
{
    public PgsqlDbContext(IConfiguration config, IDbConnection connection)
        : base(config, connection)
    {
    }
}