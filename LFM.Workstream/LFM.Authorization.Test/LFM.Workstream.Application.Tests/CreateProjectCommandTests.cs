using AutoFixture.Xunit2;
using FluentAssertions;
using LFM.WorkStream.Application.Commands;
using LFM.WorkStream.Core.Models;

namespace LFM.Authorization.Test.LFM.Workstream.Application.Tests;

public class CreateProjectCommandTests
{
    private readonly CreateProjectCommandBuilder _builder = new();

    [Theory, AutoData]
    public async Task CreateProjectCommandHandler_ShouldReturnProject_WhenProjectCreated(string workstreamId, string name, string description, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        // Arrange
        var project = new Project
        {
            WorkStreamId = workstreamId,
            Name = name,
            Description = description,
            StartDate = startDate,
            EndDate = endDate
        };
        
        var createProjectCommand = new CreateProjectCommand(workstreamId, name, description, startDate, endDate);
        var sut = _builder.WithProjectRepositoryMock(workstreamId, name, description, startDate, endDate, project).Build();

        // Act
        var result = await sut.Handle(createProjectCommand, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}