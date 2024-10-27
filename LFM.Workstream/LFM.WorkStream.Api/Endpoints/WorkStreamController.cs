using FluentValidation;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.AspNetCore.Services;
using LFM.WorkStream.Api.Authorization;
using LFM.WorkStream.Api.Endpoints.Dto;
using LFM.WorkStream.Api.Validators;
using LFM.WorkStream.Application.Commands;
using LFM.WorkStream.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LFM.WorkStream.Api.Endpoints;

[ApiController]
[Route("workstreams")]
public class WorkStreamController(ISender sender, WorkStreamValidator validator) : ControllerBase
{
    [HttpPost]
    [LfmAuthorize]
    public async Task<IResult> CreateWorkStream(WorkStreamDto request)
    {
        ValidateDto(request);
        var result = await sender.Send(new CreateWorkStreamCommand(request.Name, request.Description, request.StartDate, request.State));
        return Results.Ok(result);
    }
    
    [HttpPut("{workstreamId}/status")]
    [LfmAuthorize([Permissions.WorkstreamConfigurer], [ScopeHelper.ScopeMaskWorkStream])]
    public async Task<IResult> UpdateWorkStreamStatus([FromRoute] string workstreamId, [FromBody] WorkStreamStatusDto request)
    {
        var result = await sender.Send(new UpdateWorkStreamStatusCommand(workstreamId, request.newState));
        return Results.Ok(result);
    }

    [HttpGet("{workstreamId}", Name = "GetWorkStream")]
    [LfmAuthorize([Permissions.WorkstreamReader], [ScopeHelper.ScopeMaskWorkStream])]
    public async Task<IResult> GetWorkStream(string workstreamId)
    {
        var result = await sender.Send(new GetWorkStreamQuery(workstreamId));
        return result == null ? Results.NotFound() : Results.Ok(result);
    }

    [HttpGet("list")]
    [LfmAuthorize]
    public async Task<IResult> ListWorkStreams()
    {
        //TODO: List only workstreams that the user has access to
        var result = await sender.Send(new ListWorkStreamsQuery());
        return Results.Ok(result);
    }

    [HttpPut("{workstreamId}")]
    [LfmAuthorize([Permissions.WorkstreamConfigurer], [ScopeHelper.ScopeMaskWorkStream])]
    public async Task<IResult> UpdateWorkStream(string workstreamId, WorkStreamDto request)
    {
        var result = await sender.Send(new UpdateWorkStreamCommand(workstreamId, request.Name, request.Description));
        return Results.Ok(result);
    }

    [HttpGet("{workstreamId}/projects")]
    [LfmAuthorize([Permissions.WorkstreamReader], [ScopeHelper.ScopeMaskWorkStream])]
    public async Task<IResult> ListWorkStreamProjects(string workstreamId)
    {
        //TODO: List only projects that the user has access to
        var result = await sender.Send(new ListWorkStreamProjectsQuery(workstreamId));
        return Results.Ok(result);
    }

    private void ValidateDto(WorkStreamDto dto)
    {
        var result = validator.Validate(dto);
        if (!result.IsValid)
        {   
            throw new ValidationException(result.Errors);
        }
    }
}