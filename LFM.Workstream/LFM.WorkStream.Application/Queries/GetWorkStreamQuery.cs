using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Queries;

public record GetWorkStreamQuery(string Id) : IRequest<Core.Models.WorkStream?>;

public class GetWorkStreamQueryHandler(IWorkStreamRepository workStreamRepository) : IRequestHandler<GetWorkStreamQuery, Core.Models.WorkStream?>
{
    public async Task<Core.Models.WorkStream?> Handle(GetWorkStreamQuery request, CancellationToken cancellationToken)
    {
        return await workStreamRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}