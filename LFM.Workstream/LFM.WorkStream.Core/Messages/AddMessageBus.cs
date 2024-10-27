using System.Reflection;
using LFM.Authorization.Core.Messages;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.WorkStream.Core.Messages;

public static class MassTransitExtension
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration, bool enableQueueListener)
    {
        services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            x.AddConsumers(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                var host = configuration["RabbitMq:Host"] ??
                           throw new NullReferenceException("RabbitMq:Host is not defined");

                cfg.Host(host, x => {});

                cfg.UseSendFilter(typeof(SendWorkstreamIdFilter<>), context);

                if (enableQueueListener)
                {
                    var queueName = configuration["RabbitMq:QueueName"] ??
                                    throw new NullReferenceException("RabbitMq:QueueName is not defined");

                    cfg.ReceiveEndpoint(queueName, e =>
                    {
                        e.ConfigureConsumers(context);
                    });
                }
            });
        });

        services.Configure<MassTransitHostOptions>(x =>
        {
            x.WaitUntilStarted = true;
        });

        return services;
    }
}