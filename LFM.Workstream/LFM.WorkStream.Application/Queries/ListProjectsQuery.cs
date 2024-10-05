using LFM.WorkStream.Core.Models;
using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Queries;

public record ListProjectsQuery : IRequest<IEnumerable<Project>>;

public class ListProjectsQueryHandler(IProjectRepository projectRepository) : IRequestHandler<ListProjectsQuery, IEnumerable<Project>>
{
    public async Task<IEnumerable<Project>> Handle(ListProjectsQuery request, CancellationToken cancellationToken)
    {
        return await projectRepository.ListAsync(cancellationToken);
    }
}