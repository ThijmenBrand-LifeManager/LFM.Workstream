using LFM.WorkStream.Core.Messages;
using LFM.WorkStream.Core.Messages.Services;
using LFM.WorkStream.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.WorkStream.Core;

public static class CoreModule
{
    public static IServiceCollection AddCoreModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddSingleton<IUserHelper, UserHelper>();
        
        services.AddOptions<RabbitmqOptions>().Configure(x =>
        {
            x.WorkStreamQueue = configuration["RabbitMq:QueueName"] ??
                                throw new NullReferenceException("RabbitMq:QueueName is not defined");
        });
        
        return services;
    }
}