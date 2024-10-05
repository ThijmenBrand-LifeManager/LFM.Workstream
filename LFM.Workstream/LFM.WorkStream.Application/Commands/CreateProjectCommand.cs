using LFM.WorkStream.Core.Models;
using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Commands;

public record CreateProjectCommand(
    string? WorkStreamId,
    string Name,
    string? Desciption,
    DateTimeOffset? StartDate,
    DateTimeOffset? EndDate) : IRequest<Project>;

public class CreateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand, Project>
{
    public async Task<Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var result = await projectRepository.CreateAsync(new Project()
        {
            Name = request.Name,
            WorkStreamId = request.WorkStreamId,
            Description = request.Desciption,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        }, cancellationToken);

        return result;
    }
}