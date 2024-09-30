using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Commands;

public record UpdateWorkStreamCommand(string Id, string Name, string? Description) : IRequest<Core.Models.WorkStream>;

public class UpdateWorkStreamCommandHandler(IWorkStreamRepository workStreamRepository) : IRequestHandler<UpdateWorkStreamCommand, Core.Models.WorkStream>
{
    public async Task<Core.Models.WorkStream> Handle(UpdateWorkStreamCommand request,
        CancellationToken cancellationToken)
    {
        var workStream = new Core.Models.WorkStream()
        {
            Id = Guid.Parse(request.Id),
            Name = request.Name,
            Description = request.Description
        };
        
        return await workStreamRepository.UpdateAsync(workStream, cancellationToken);
    }
}