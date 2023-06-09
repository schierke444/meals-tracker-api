using System.Reflection;
using BuildingBlocks.EFCore;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Features.Posts.Repositories;
using Posts.Persistence;

namespace Posts.Extensions;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddCustomDbContext<PostsDbContext>();
        services.AddJwtExtensions(config);
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();

        return services;
    }
}
