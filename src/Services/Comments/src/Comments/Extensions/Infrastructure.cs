using System.Reflection;
using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Dapper;
using BuildingBlocks.EFCore;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using Comments.Commons.Interfaces;
using Comments.Features.Comments.Interfaces;
using Comments.Features.Comments.Repositories;
using Comments.Features.Likes.Interfaces;
using Comments.Features.Likes.Repositories;
using Comments.Persistence;
using Comments.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Repositories;

namespace Comments.Extensions;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddCustomDbContext<CommentsDbContext>(config);
        services.AddJwtExtensions(config);
        services.AddScoped<IPgsqlDbContext, PgsqlDbContext>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<IUsersCommentsRepository, UsersCommentsRepository>();
        services.AddScoped<IUsersCommentsService, UsersCommentsService>();
        services.AddScoped<ILikeCommentRepository, LikeCommentRepository>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();
        services.AddMassTransitExtension(config);

        return services;
    }
}
