using Azure;
using Azure.Data.Tables;
using LFM.WorkStream.Core;

namespace LFM.WorkStream.Repository.TableRepository;

public class TableRepositoryBase : IInitializer
{
    private readonly TableServiceClient _tableServiceClient;
    private readonly string _tableName;
    
    protected TableClient TableClient { get; init; }
    
    protected TableRepositoryBase(TableServiceClient tableServiceClient, string tableName)
    {
        _tableServiceClient = tableServiceClient;
        _tableName = tableName;
        TableClient = _tableServiceClient.GetTableClient(_tableName);
    }

    public async Task Initialize()
    {
        await _tableServiceClient.CreateTableIfNotExistsAsync(_tableName);
    }
}

public abstract class TableRepositoryBase<TObj, TEntity> : TableRepositoryBase
    where TEntity : class, ITableEntity, new()
{
    protected TableRepositoryBase(TableServiceClient tableServiceClient, string tableName) : base(tableServiceClient, tableName) {}

    public async Task<TObj> CreateOrUpdateAsync(TObj obj, CancellationToken cancellationToken = default)
    {
        var result = await TableClient.UpsertEntityAsync(ToEntity(obj), cancellationToken: cancellationToken);
        if (result.IsError)
            throw new Exception("Error while inserting object");

        return obj;
    }

    public async Task<TObj?> GetIfExistsAsync(string partitionKey, string rowKey, CancellationToken cancellationToken = default)
    {
        var response =
            await TableClient.GetEntityIfExistsAsync<TEntity>(partitionKey, rowKey,
                cancellationToken: cancellationToken);

        return (response.HasValue ? ToObject(response.Value) : default)!;
    }

    protected abstract TEntity ToEntity(TObj ob);
    protected abstract TObj ToObject(TEntity entity);
}