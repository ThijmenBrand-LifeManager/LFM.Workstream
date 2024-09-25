using Azure.Data.Tables;
using LFM.WorkStream.Repository.Entities;
using LFM.WorkStream.Repository.Interfaces;

namespace LFM.WorkStream.Repository.TableRepository.WorkStreamTableRepository;

public class WorkStreamTableRepository(TableServiceClient tableServiceClient)
    : TableRepositoryBase<Core.Models.WorkStream, WorkStreamEntity>(tableServiceClient, TableName),
        IWorkStreamTableRepository
{
    private const string TableName = "WorkStreams";

    protected override WorkStreamEntity ToEntity(Core.Models.WorkStream obj) => new()
    {
        PartitionKey = obj.Id.ToString(),
        RowKey = obj.Id.ToString(),
        Name = obj.Name,
        Description = obj.Description
    };
    
    protected override Core.Models.WorkStream ToObject(WorkStreamEntity entity) => new()
    {
        Id = Guid.Parse(entity.PartitionKey),
        Name = entity.Name,
        Description = entity.Description
    };
}