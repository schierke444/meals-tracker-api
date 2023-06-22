using System.Data;

namespace BuildingBlocks.Commons.Interfaces;

public interface IPgsqlDbContext : IReadDbContext 
{
    IDbConnection _pgConnection { get;} 
}