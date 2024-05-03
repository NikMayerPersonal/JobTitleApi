using Application.Jobs.Query.GetJobById;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.Api.Controllers.v1.Jobs
{
    /// <summary>
    /// Controller for Job entity
    /// </summary>
    [ApiVersion("1")]
    public class JobController : BaseController
    {
        private readonly IValidator<GetJobByIdQuery> _getJobByIdRequestValidator;

        public JobController(IValidator<GetJobByIdQuery> getJobByIdRequestValidator)
        {
            _getJobByIdRequestValidator = getJobByIdRequestValidator;
        }

        /// <summary>
        /// Gets a job by id
        /// </summary>
        /// <param name="request">A request of type GetJobByIdRequest</param>
        /// <returns>Returns an object of type JobQueryModel</returns>
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetJobByIdQuery request)
        {
            
            var validationResult = _getJobByIdRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }
            var result = await Mediator.Send(request);

            if(result == null)
            {
                return NotFound($"Job with Id: {request.JobId} doesn't exist");
            }

            return Ok(result);
        }
    }
}
