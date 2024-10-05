using LFM.WorkStream.Api.Endpoints.Dto;
using LFM.WorkStream.Application.Commands;
using LFM.WorkStream.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LFM.WorkStream.Api.Endpoints;

[ApiController]
[Route("[controller]")]
public class ProjectController(ISender sender) : ControllerBase
{
    [HttpGet("{id}", Name = "GetProject")]
    public async Task<IResult> GetProject(string id)
    {
        var result = await sender.Send(new GetProjectQuery(id));
        return result == null ? Results.NotFound() : Results.Ok(result);
    }
    
    [HttpPost(Name = "CreateProject")]
    public async Task<IResult> CreateProject(ProjectDto project)
    {
        var result = await sender.Send(new CreateProjectCommand(project.WorkStreamId, project.Name, project.Description,
            project.StartDate, project.EndDate));

        return Results.Ok(result);
    }

    [HttpPut( "{id}", Name = "UpdateProject")]
    public async Task<IResult> UpdateProject([FromRoute] string id, [FromBody] ProjectDto project)
    {
        var result = await sender.Send(new UpdateProjectCommand(id, project.WorkStreamId, project.Name,
            project.Description, project.StartDate, project.EndDate));

        return Results.Ok(result);
    }

    [HttpGet("list", Name = "ListProjects")]
    public async Task<IResult> ListProjects()
    {
        var result = await sender.Send(new ListProjectsQuery());
        return Results.Ok(result);
    }
}