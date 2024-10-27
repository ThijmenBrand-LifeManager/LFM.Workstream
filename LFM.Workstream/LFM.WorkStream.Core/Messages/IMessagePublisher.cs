namespace LFM.WorkStream.Core.Messages.Services;

public interface IMessagePublisher
{
    void SendToQueue(object command, CancellationToken cancellationToken);
    void PublishToTopic(object message, CancellationToken cancellationToken);
}