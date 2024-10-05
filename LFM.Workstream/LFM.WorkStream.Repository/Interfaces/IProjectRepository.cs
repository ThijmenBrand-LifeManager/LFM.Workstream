using LFM.WorkStream.Core.Models;

namespace LFM.WorkStream.Repository.Interfaces;

public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project, CancellationToken cancellationToken);
    Task<Project> UpdateAsync(Project project, CancellationToken cancellationToken);
    Task<Project> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Project>> ListAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Project>> ListWorkStreamProjectsAsync(string workStreamId, CancellationToken cancellationToken);
}