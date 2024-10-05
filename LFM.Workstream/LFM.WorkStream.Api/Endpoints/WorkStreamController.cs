using FluentValidation;
using LFM.Authorization.AspNetCore;
using LFM.WorkStream.Api.Endpoints.Dto;
using LFM.WorkStream.Api.Validators;
using LFM.WorkStream.Application.Commands;
using LFM.WorkStream.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFM.WorkStream.Api.Endpoints;

[ApiController]
[Route("[controller]")]
public class WorkStreamController(ISender sender, WorkStreamValidator validator) : ControllerBase
{
    [HttpPost(Name = "CreateWorkStream")]
    public async Task<IResult> CreateWorkStream(WorkStreamDto request)
    {
        ValidateDto(request);
        var result = await sender.Send(new CreateWorkStreamCommand(request.Name, request.Description));
        return Results.Ok(result);
    }

    [HttpGet("{id}", Name = "GetWorkStream")]
    public async Task<IResult> GetWorkStream(string id)
    {
        var result = await sender.Send(new GetWorkStreamQuery(id));
        return result == null ? Results.NotFound() : Results.Ok(result);
    }

    [HttpGet("list", Name = "ListWorkStreams")]
    [LfmAuthorize]
    public async Task<IResult> ListWorkStreams()
    {
        var result = await sender.Send(new ListWorkStreamsQuery());
        return Results.Ok(result);
    }

    [HttpPut("{id}", Name = "UpdateWorkStream")]
    public async Task<IResult> UpdateWorkStream(string id, WorkStreamDto request)
    {
        var result = await sender.Send(new UpdateWorkStreamCommand(id, request.Name, request.Description));
        return Results.Ok(result);
    }

    [HttpGet("{id}/projects", Name = "ListWorkStreamProjects")]
    public async Task<IResult> ListWorkStreamProjects(string id)
    {
        var result = await sender.Send(new ListWorkStreamProjectsQuery(id));
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