using System.Data;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace BuildingBlocks.EFCore;

public static class Extensions
{
    public static IServiceCollection AddCustomDbContext<TContext>(this IServiceCollection services, IConfiguration config)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(opt => opt.UseNpgsql(config.GetConnectionString("DB")));

        // Connections
        services.AddTransient<IDbConnection>(opt => new NpgsqlConnection(
            config.GetConnectionString("DB")
        ));
        return services;
    }
}