using LFM.WorkStream.Application.Commands;
using LFM.WorkStream.Core.Enums;
using LFM.WorkStream.Core.Messages.Events;
using LFM.WorkStream.Core.Messages.Services;
using LFM.WorkStream.Core.Utils;
using LFM.WorkStream.Repository.Interfaces;
using Moq;

namespace LFM.WorkStream.Tests.ApplicationTests.CreateWorkstreamCommandTests;

public class CreateWorkstreamCommandTestBuilder
{
    private readonly Mock<IWorkStreamRepository> _workStreamRepositoryMock = new();
    private readonly Mock<IMessagePublisher> _messagePublisherMock = new();
    private readonly Mock<IUserHelper> _userHelperMock = new();

    public CreateWorkStreamCommandHandler Build() =>
        new CreateWorkStreamCommandHandler(_workStreamRepositoryMock.Object, _messagePublisherMock.Object, _userHelperMock.Object);
    
    public CreateWorkstreamCommandTestBuilder WithWorkstreamRespository(
        string name, string? description, DateTimeOffset? startDate, WorkstreamState? state, Core.Models.WorkStream output)
    {
        _workStreamRepositoryMock.Setup(func => func.CreateAsync(
                It.Is<Core.Models.WorkStream>(
                    x => x.Name == name && 
                         x.Description == description && 
                         x.StartDate == startDate && 
                         x.State == state), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(output);
        return this;
    }
    
    public CreateWorkstreamCommandTestBuilder WithMessagePublisher(string workstreamId, string creatorId)
    {
        _messagePublisherMock.Setup(func => func.SendToQueue(
            It.Is<WorkstreamCreatedEvent>(x => x.WorkstreamId == workstreamId && x.CreatorId == creatorId), 
            It.IsAny<CancellationToken>()));
        return this;
    }
    
    public CreateWorkstreamCommandTestBuilder WithUserHelper(string userId)
    {
        _userHelperMock.Setup(func => func.GetUserId()).Returns(userId);
        return this;
    }
}