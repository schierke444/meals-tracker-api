using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meals.Extensions;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit((cfg) => {
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.UsingRabbitMq((ctx, cfg) => {
                cfg.Host(config["EventBusSettings:HostAddress"]);
            });
        });
        return services;   
    }    
}