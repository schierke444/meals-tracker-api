
using System.Reflection;
using BuildingBlocks.Commons.Behaviors;
using BuildingBlocks.Events;
using BuildingBlocks.Events.Users;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.API.RequestConsumers;
using Users.Features.Users.RequestConsumers;

namespace Users.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddConsumer<GetUserByIdConsumer>();
            cfg.AddRequestClient<GetUserByIdRecord>(new Uri("exchange:getuser-queue"));

            cfg.AddConsumer<CreateUserRecordConsumer>();
            cfg.AddRequestClient<CreateUserRecord>(new Uri("exchange:createuser-queue"));

            // Specifying the Event to be consume
            cfg.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(config["EventBusSettings:HostAddress"]);
                cfg.ReceiveEndpoint("getuser-queue", (c) =>
                {
                    c.ConfigureConsumer<GetUserByIdConsumer>(ctx);
                });

                cfg.ReceiveEndpoint("create-queue", (c) =>
                {
                    c.ConfigureConsumer<CreateUserRecordConsumer>(ctx);
                });
            });
        });
        return services;
    }
}