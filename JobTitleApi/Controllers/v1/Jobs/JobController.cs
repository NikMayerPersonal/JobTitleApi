using Application.Jobs.Query.GetJobById;
using FluentValidation;
using Jobs.Api.Controllers.v1.Jobs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.Api.Controllers.v1.Jobs
{
    [ApiVersion("1")]
    public class JobController : BaseController
    {
        private readonly IValidator<GetJobByIdRequest> _getJobByIdRequestValidator;

        public JobController(IValidator<GetJobByIdRequest> getJobByIdRequestValidator)
        {
            _getJobByIdRequestValidator = getJobByIdRequestValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetJobByIdRequest request)
        {
            
            var validationResult = _getJobByIdRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }
            var result = await Mediator.Send(new GetJobByIdQuery { JobId = request.JobId });

            if(result == null)
            {
                return NotFound($"Job with Id: {request.JobId} doesn't exist");
            }
            return Ok(result);
        }
    }
}
