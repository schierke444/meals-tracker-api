using System.Reflection;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Dapper;
using BuildingBlocks.EFCore;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using FluentValidation;
using Meals.Commons.Interfaces;
using Meals.Features.Category;
using Meals.Features.Category.Repositories;
using Meals.Features.Ingredients.Interfaces;
using Meals.Features.Ingredients.Repositories;
using Meals.Features.Likes.Interfaces;
using Meals.Features.Likes.Repositories;
using Meals.Features.Meals.Interfaces;
using Meals.Features.Meals.Repositories;
using Meals.Features.Meals.Services;
using Meals.Persistence;
using Meals.Repositories;
using Meals.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Repositories;

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
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMealCategoryRepository, MealCategoryRepository>();
        services.AddScoped<IUsersMealsRepository, UsersMealsRepository>();
        services.AddScoped<ILikeMealsRepository, LikeMealsRepository>();
        services.AddTransient<IUsersMealsService, UsersMealsService>();
        services.AddTransient<MealCategoryService>();
        services.AddTransient<MealIngredientsService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddApiVersionExtension();
        services.AddCustomMediatR();
        services.AddMassTransitExtension(config);

        return services;
    }
}