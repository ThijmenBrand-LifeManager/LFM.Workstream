using LFM.WorkStream.Repository.Interfaces;
using MediatR;

namespace LFM.WorkStream.Application.Commands;

public record CreateWorkStreamCommand(string Name, string? Description) : IRequest<Core.Models.WorkStream>;

public class CreateWorkStreamCommandHandler(IWorkStreamTableRepository workStreamTableRepository) : IRequestHandler<CreateWorkStreamCommand, Core.Models.WorkStream>
{
    public async Task<Core.Models.WorkStream> Handle(CreateWorkStreamCommand request,
        CancellationToken cancellationToken)
    {
        var workStream = new Core.Models.WorkStream()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
        };

        return await workStreamTableRepository.CreateOrUpdateAsync(workStream, cancellationToken);
    }
}