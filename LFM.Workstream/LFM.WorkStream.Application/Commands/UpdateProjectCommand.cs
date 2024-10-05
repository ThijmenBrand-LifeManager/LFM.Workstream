using LFM.WorkStream.Core.Models;
using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Commands;

public record UpdateProjectCommand(string Id, string? WorkStreamId, string Name, string? Description, DateTimeOffset? StartDate, DateTimeOffset? EndDate) : IRequest<Project>;

public class UpdateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand, Project>
{
    public async Task<Project> Handle(UpdateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var workStream = new Project()
        {
            Id = Guid.Parse(request.Id),
            WorkStreamId = request.WorkStreamId,
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        
        return await projectRepository.UpdateAsync(workStream, cancellationToken);
    }
}