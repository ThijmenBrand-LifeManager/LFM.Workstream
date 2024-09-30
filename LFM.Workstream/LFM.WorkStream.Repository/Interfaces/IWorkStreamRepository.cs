
namespace LFM.WorkStream.Repository.Interfaces;

public interface IWorkStreamRepository
{
    Task<Core.Models.WorkStream> CreateAsync(Core.Models.WorkStream workStream, CancellationToken cancellationToken);
    Task<Core.Models.WorkStream> UpdateAsync(Core.Models.WorkStream workStream, CancellationToken cancellationToken);
    Task<Core.Models.WorkStream> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Core.Models.WorkStream>> ListAsync(CancellationToken cancellationToken);
}