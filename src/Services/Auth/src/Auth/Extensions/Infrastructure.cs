﻿using Auth.Persistence;
using Auth.Repositories;
using Auth.Services;
using BuildingBlocks.EFCore;
using BuildingBlocks.Jwt;
using BuildingBlocks.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.Extensions;
public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddCustomDbContext<AuthDbContext>();
        services.AddJwtExtensions(config);
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCustomMediatR();


        return services;
    }
}
