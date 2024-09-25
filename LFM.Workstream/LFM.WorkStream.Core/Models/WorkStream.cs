namespace LFM.WorkStream.Core.Models;

public class WorkStream
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}