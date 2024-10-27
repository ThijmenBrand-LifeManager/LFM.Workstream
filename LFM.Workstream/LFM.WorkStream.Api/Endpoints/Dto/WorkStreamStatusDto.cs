using LFM.WorkStream.Core.Enums;

namespace LFM.WorkStream.Api.Endpoints.Dto;

public class WorkStreamStatusDto
{
    public WorkstreamState newState { get; set; }
}