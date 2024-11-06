using AutoFixture.Xunit2;
using FluentAssertions;
using LFM.WorkStream.Application.Commands;
using LFM.WorkStream.Core.Enums;

namespace LFM.WorkStream.Tests.ApplicationTests.CreateWorkstreamCommandTests;

public class CreateWorkstreamCommandTest
{
    private readonly CreateWorkstreamCommandTestBuilder _builder = new();
    
    [Theory]
    [InlineAutoData]
    public async void CreateWorkstreamCommandHandler_ShouldCreateWorkstream_WhenStartDateIsNotPassed(
        Guid workstreamId, string name, string description, DateTimeOffset startDate, WorkstreamState state, string userId)
    {
        // Arrange
        var output = new Core.Models.WorkStream
        {
            Id = workstreamId,
            Name = name,
            Description = description,
            StartDate = startDate,
            State = state,
        };
        
        var sut = _builder
            .WithWorkstreamRespository(name, description, startDate, state, output)
            .WithMessagePublisher(workstreamId.ToString(), userId)
            .WithUserHelper(userId)
            .Build();
        
        var command = new CreateWorkStreamCommand(name, description, startDate, state);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull().And.BeEquivalentTo(output);
    }
}