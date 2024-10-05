using LFM.WorkStream.Core.Models;
using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Queries;

public record ListWorkStreamProjectsQuery(string WorkStreamId) : IRequest<IEnumerable<Project>>;

public class ListWorkStreamProjectsQueryHandler(IProjectRepository projectRepository) : IRequestHandler<ListWorkStreamProjectsQuery, IEnumerable<Project>>
{
    public async Task<IEnumerable<Project>> Handle(ListWorkStreamProjectsQuery request, CancellationToken cancellationToken)
    {
        var result = await projectRepository.ListWorkStreamProjectsAsync(request.WorkStreamId, cancellationToken);
        return result;
    }
}