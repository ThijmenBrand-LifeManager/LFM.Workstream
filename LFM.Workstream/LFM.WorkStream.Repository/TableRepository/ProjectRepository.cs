using LFM.WorkStream.Core.Models;
using LFM.WorkStream.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LFM.WorkStream.Repository.TableRepository;

public class ProjectRepository(DatabaseContext context) : RepositoryBase<Project>(context), IProjectRepository
{
    public async Task<IEnumerable<Project>> ListWorkStreamProjectsAsync(string workStreamId,
        CancellationToken cancellationToken)
    {
        var result = Context.Projects.Local.Where(p => p.WorkStreamId == workStreamId).ToList();
        if (result.Count == 0)
        {
            result = await Context.Projects.Where(p => p.WorkStreamId == workStreamId).ToListAsync(cancellationToken);
        }

        return result;
    }
}