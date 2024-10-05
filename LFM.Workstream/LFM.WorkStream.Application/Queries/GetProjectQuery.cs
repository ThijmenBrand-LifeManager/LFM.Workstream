using System.Drawing;
using LFM.WorkStream.Core.Models;
using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Queries;

public record GetProjectQuery(string Id) : IRequest<Project?>;

public class GetProjectQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectQuery, Project?>
{
    public async Task<Project?> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        return await projectRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}