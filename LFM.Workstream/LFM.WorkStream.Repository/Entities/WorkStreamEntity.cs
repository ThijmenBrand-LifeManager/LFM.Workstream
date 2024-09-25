using Azure;
using Azure.Data.Tables;

namespace LFM.WorkStream.Repository.Entities;

public class WorkStreamEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}