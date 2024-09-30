using FluentValidation;
using LFM.WorkStream.Api.Endpoints.Dto;

namespace LFM.WorkStream.Api.Validators;

public class WorkStreamValidator : AbstractValidator<WorkStreamDto>
{
    public WorkStreamValidator()
    {
        RuleFor(workStream => workStream.Name).NotNull().NotEmpty().Length(3, 100);
        RuleFor(workStream => workStream.Description).MaximumLength(500);
    }
}