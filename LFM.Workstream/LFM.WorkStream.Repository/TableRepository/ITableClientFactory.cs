using Azure.Data.Tables;

namespace LFM.WorkStream.Repository.TableRepository;

public interface ITableClientFactory
{
    TableServiceClient CreateTableClientByConnectionString(string connectionString);
    TableServiceClient CreateTableClientByStorageAccountName(string storageAccountName);
}