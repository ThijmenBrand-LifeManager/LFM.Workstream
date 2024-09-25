
namespace LFM.WorkStream.Repository.Interfaces;

public interface IWorkStreamTableRepository
{
    Task<Core.Models.WorkStream> CreateOrUpdateAsync(Core.Models.WorkStream workStream, CancellationToken cancellationToken);
}