using System.Reflection;
using Azure.Core;
using LFM.Authorization.Core.Messages;
using LFM.Azure.Common.Helpers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.WorkStream.Core.Messages;

public static class MassTransitExtension
{
    public static IServiceCollection RegisterMasstransit(this IServiceCollection services, IConfiguration configuration, bool enableQueueListener)
    {
        var sp = services.BuildServiceProvider();
        var tokencredential = sp.GetService<TokenCredential>();
        
        services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            x.AddConsumers(entryAssembly);

            x.UsingAzureServiceBus((context, cfg) =>
            {
                    var connectionString = configuration["ServiceBus:ConnectionString"] ??
                                           throw new NullReferenceException("ServiceBus:ConnectionString is not defined");
                    cfg.Host(connectionString);

                cfg.UseSendFilter(typeof(SendWorkstreamIdFilter<>), context);

                if (enableQueueListener)
                {
                    var workstreamQueue = configuration["ServiceBus:WorkstreamQueueName"] ??
                                          throw new NullReferenceException("ServiceBus:WorkstreamQueueName is not defined");

                    cfg.ReceiveEndpoint(workstreamQueue, e =>
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