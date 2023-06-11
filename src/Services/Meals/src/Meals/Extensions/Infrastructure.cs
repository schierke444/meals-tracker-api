using System.Reflection;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Dapper;
using BuildingBlocks.EFCore;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using FluentValidation;
using Meals.Commons.Interfaces;
using Meals.Features.Ingredients.Interfaces;
using Meals.Features.Ingredients.Repositories;
using Meals.Persistence;
using Meals.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meals.Extensions;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddCustomDbContext<MealsDbContext>(config);
        services.AddJwtExtensions(config);
        services.AddScoped<IPgsqlDbContext, PgsqlDbContext>();
        services.AddScoped<IMealsRepository, MealsRepository>();
        services.AddScoped<IIngredientsRepository, IngredientsRepository>();
        services.AddScoped<IMealIngredientsRepository, MealIngredientsRepository>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();

        return services;
    }
}