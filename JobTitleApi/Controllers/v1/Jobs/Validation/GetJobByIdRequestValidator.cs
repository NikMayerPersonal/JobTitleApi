using FluentValidation;
using Jobs.Api.Controllers.v1.Jobs.Requests;

namespace Jobs.Api.Controllers.v1.Jobs.Validation
{
    public class GetJobByIdRequestValidator : AbstractValidator<GetJobByIdRequest>
    {
        public GetJobByIdRequestValidator()
        {
            RuleFor(x => x.JobId)
                .NotNull().NotEmpty().GreaterThan(0).WithMessage("{PropertyName} is not valid");
        }
    }
}
