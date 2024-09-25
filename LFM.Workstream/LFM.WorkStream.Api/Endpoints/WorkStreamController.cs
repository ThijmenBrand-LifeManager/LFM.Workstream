using Carter;
using FluentValidation;
using LFM.WorkStream.Api.Endpoints.Dto;
using LFM.WorkStream.Api.Validators;
using LFM.WorkStream.Application.Commands;
using MediatR;

namespace LFM.WorkStream.Api.Endpoints;

public class WorkStreamController(ISender sender, WorkStreamValidator validator) : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("workstream", CreateWorkStream);
    }

    public async Task<IResult> CreateWorkStream(WorkStreamDto request)
    {
        ValidateDto(request);
        var result = await sender.Send(new CreateWorkStreamCommand(request.Name, request.Description));
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