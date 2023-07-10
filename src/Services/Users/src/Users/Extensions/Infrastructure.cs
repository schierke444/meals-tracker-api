using System.Reflection;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Dapper;
using BuildingBlocks.EFCore;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Features.Roles.Interfaces;
using Users.Features.Roles.Repositories;
using Users.Features.Users.Interfaces;
using Users.Features.Users.Repositories;
using Users.Persistence;

namespace Users.Extensions;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddCustomDbContext<ApplicationDbContext>(config);
        services.AddJwtExtensions(config);
        services.AddScoped<IPgsqlDbContext, PgsqlDbContext>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();
        services.AddMassTransitExtensions(config);
        return services;
    } 
}