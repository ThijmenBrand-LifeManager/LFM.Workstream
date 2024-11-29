using System.Reflection;
using Azure.Core;
using Azure.Identity;
using LFM.Authorization.Core.Messages;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DefaultAzureCredential = Azure.Identity.DefaultAzureCredential;

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
                var host = configuration["ServiceBus:Host"] ??
                           throw new NullReferenceException("ServiceBus:Host is not defined");
                cfg.Host(new Uri(host), h =>
                {
                    h.TokenCredential = tokencredential;
                });

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