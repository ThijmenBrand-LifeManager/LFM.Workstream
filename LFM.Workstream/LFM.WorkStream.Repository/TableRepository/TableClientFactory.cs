using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;

namespace LFM.WorkStream.Repository.TableRepository;

public class TableClientFactory() : ITableClientFactory
{
    public TableServiceClient CreateTableClientByConnectionString(string connectionString)
    {
        return new TableServiceClient(connectionString);
    }
    
    public TableServiceClient CreateTableClientByStorageAccountName(string storageAccountName)
    {
        throw new NotImplementedException("Currently does not support creating TableServiceClient by StorageAccountName");
    }
}