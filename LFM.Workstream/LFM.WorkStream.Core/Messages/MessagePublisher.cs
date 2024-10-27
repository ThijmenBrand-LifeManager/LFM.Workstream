using LFM.WorkStream.Core.Messages.Services;
using MassTransit;
using Microsoft.Extensions.Options;

namespace LFM.WorkStream.Core.Messages;

public class MessagePublisher(IBus bus, IOptions<RabbitmqOptions> options) : IMessagePublisher
{
    public async void SendToQueue(object command, CancellationToken cancellationToken = default)
    {
        var queueName = options.Value.WorkStreamQueue
                        ?? throw new ArgumentException("WorkStreamQueue is not set in the configuration");
        var sendEndpoint = await bus.GetSendEndpoint(new Uri($"queue:{queueName}"));
        await sendEndpoint.Send(command, cancellationToken);
    }
    
    public async void PublishToTopic(object message, CancellationToken cancellationToken = default)
    {
        await bus.Publish(message, cancellationToken);
    }
}