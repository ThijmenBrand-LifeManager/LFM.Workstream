using LFM.WorkStream.Core.Enums;
using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Commands;

public record UpdateWorkStreamStatusCommand(string WorkStreamId, WorkstreamState NewState) : IRequest<Core.Models.WorkStream>;

public class UpdateWorkStreamStatusCommandHandler(IWorkStreamRepository workStreamRepository) : IRequestHandler<UpdateWorkStreamStatusCommand, Core.Models.WorkStream>
{
    public async Task<Core.Models.WorkStream> Handle(UpdateWorkStreamStatusCommand request, CancellationToken cancellationToken)
    {
        var workStream = await workStreamRepository.GetByIdAsync(request.WorkStreamId, cancellationToken);
        if (workStream == null)
        {
            throw new Exception(nameof(Core.Models.WorkStream));
        }

        workStream.State = request.NewState;

        return await workStreamRepository.UpdateAsync(workStream, cancellationToken);
    }
    
}