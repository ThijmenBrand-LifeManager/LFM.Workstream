using LFM.WorkStream.Application.Commands;
using LFM.WorkStream.Core.Models;
using LFM.WorkStream.Repository.Interfaces;
using Moq;

namespace LFM.Authorization.Test.LFM.Workstream.Application.Tests;

public class CreateProjectCommandBuilder
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

    public CreateProjectCommandHandler Build() => new(_projectRepositoryMock.Object);
    
    public CreateProjectCommandBuilder WithProjectRepositoryMock(string workstreamId, string name, string description, DateTimeOffset startDate, DateTimeOffset endDate, Project project)
    {
        _projectRepositoryMock.Setup(x => x.CreateAsync(It.Is<Project>(p =>
            p.WorkStreamId == workstreamId && 
            p.Name == name && 
            p.Description == description && 
            p.StartDate == startDate && 
            p.EndDate == endDate
        ), It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        return this;
     }
    
    
}