using BuildingBlocks.Events.Posts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Features.Posts.RequestConsumers;

namespace Posts.Extensions;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit((cfg) => {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.AddConsumer<GetPostsByIdConsumer>();
            cfg.AddRequestClient<GetPostsByIdRecord>(new Uri("exchange:getpost-queue"));

            cfg.UsingRabbitMq((ctx, cfg) => {
                cfg.Host(config["EventBusSettings:HostAddress"]);

                cfg.ReceiveEndpoint("getpost-queue", (c) =>
                {
                    c.ConfigureConsumer<GetPostsByIdConsumer>(ctx);
                });
            });
        });
        return services;
    } 
}