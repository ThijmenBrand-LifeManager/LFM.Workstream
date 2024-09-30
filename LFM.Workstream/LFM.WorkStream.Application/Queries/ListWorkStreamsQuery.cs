using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Queries;

public record ListWorkStreamsQuery : IRequest<IEnumerable<Core.Models.WorkStream>>;

public class ListWorkStreamsQueryHandler(IWorkStreamRepository workStreamRepository) : IRequestHandler<ListWorkStreamsQuery, IEnumerable<Core.Models.WorkStream>>
{
    public async Task<IEnumerable<Core.Models.WorkStream>> Handle(ListWorkStreamsQuery request, CancellationToken cancellationToken)
    {
        var result = await workStreamRepository.ListAsync(cancellationToken);

        return result;
    }
}