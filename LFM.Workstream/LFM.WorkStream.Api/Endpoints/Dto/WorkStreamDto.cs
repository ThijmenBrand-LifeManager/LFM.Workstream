using LFM.WorkStream.Core.Enums;

namespace LFM.WorkStream.Api.Endpoints.Dto;

public class WorkStreamDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public WorkstreamState? State { get; set; }
}