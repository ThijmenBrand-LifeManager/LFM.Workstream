namespace LFM.WorkStream.Api.Endpoints.Dto;

public class ProjectDto
{
    public string? WorkStreamId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}