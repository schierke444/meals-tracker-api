using System.Reflection;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Dapper;
using BuildingBlocks.EFCore;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Commons.Interfaces;
using Posts.Features.Likes.Interfaces;
using Posts.Features.Likes.Repositories;
using Posts.Features.Posts.Interfaces;
using Posts.Features.Posts.Repositories;
using Posts.Persistence;
using Posts.Repositories;
using Posts.Services;

namespace Posts.Extensions;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddCustomDbContext<PostsDbContext>(config);
        services.AddJwtExtensions(config);
        services.AddScoped<IPgsqlDbContext, PgsqlDbContext>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUsersPostsRepository, UsersPostsRepository>();
        services.AddScoped<ILikePostsRepository, LikePostsRepository>();
        services.AddScoped<IUsersPostsService, UsersPostsService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();
        services.AddMassTransitExtension(config);

        return services;
    }
}
