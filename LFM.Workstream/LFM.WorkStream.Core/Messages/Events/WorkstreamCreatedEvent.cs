namespace LFM.WorkStream.Core.Messages.Events;

public record WorkstreamCreatedEvent(string WorkstreamId, string CreatorId);