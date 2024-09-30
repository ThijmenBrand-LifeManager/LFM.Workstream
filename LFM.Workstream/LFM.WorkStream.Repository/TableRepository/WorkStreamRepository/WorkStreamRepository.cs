using LFM.WorkStream.Repository.Interfaces;

namespace LFM.WorkStream.Repository.TableRepository.WorkStreamRepository;

public class WorkStreamRepository(DatabaseContext dbContext) : RepositoryBase<Core.Models.WorkStream>(dbContext), IWorkStreamRepository
{
}