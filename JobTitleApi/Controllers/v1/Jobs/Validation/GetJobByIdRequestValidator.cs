using Application.Jobs.Query.GetJobById;
using FluentValidation;

namespace Jobs.Api.Controllers.v1.Jobs.Validation
{
    /// <summary>
    /// A validator for GetJobById requests
    /// </summary>
    public class GetJobByIdRequestValidator : AbstractValidator<GetJobByIdQuery>
    {
        public GetJobByIdRequestValidator()
        {
            RuleFor(x => x.JobId)
                .NotNull().NotEmpty().GreaterThanOrEqualTo(0).WithMessage("JobId is not valid");
        }
    }
}
