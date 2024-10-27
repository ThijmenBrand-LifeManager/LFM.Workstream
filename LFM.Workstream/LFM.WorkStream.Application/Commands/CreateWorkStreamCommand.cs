using LFM.WorkStream.Core.Enums;
using LFM.WorkStream.Core.Messages.Events;
using LFM.WorkStream.Core.Messages.Services;
using LFM.WorkStream.Core.Utils;
using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Commands;

public record CreateWorkStreamCommand(string Name, string? Description, DateTimeOffset? StartDate, WorkstreamState? State) : IRequest<Core.Models.WorkStream>;

public class CreateWorkStreamCommandHandler(IWorkStreamRepository workStreamRepository, IMessagePublisher messagePublisher, IUserHelper userHelper) : IRequestHandler<CreateWorkStreamCommand, Core.Models.WorkStream>
{
    public async Task<Core.Models.WorkStream> Handle(CreateWorkStreamCommand request,
        CancellationToken cancellationToken)
    {
        var state = request.State ?? WorkstreamState.NotStarted;
        if (request.StartDate >= DateTimeOffset.Now)
        {
            state = WorkstreamState.InProgress;
        }
        
        var workStream = new Core.Models.WorkStream()
        {
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            State = state,
        };
        //TODO: Publish event to add roleassignment for the user who created the workstream

        var result = await workStreamRepository.CreateAsync(workStream, cancellationToken);

        var userId = userHelper.GetUserId();
        messagePublisher.SendToQueue(new WorkstreamCreatedEvent(result.Id.ToString(), userId), cancellationToken);
        
        return result;
    }
}