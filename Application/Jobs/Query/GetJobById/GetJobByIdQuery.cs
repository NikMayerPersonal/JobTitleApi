using MediatR;

namespace Application.Jobs.Query.GetJobById
{
    public class GetJobByIdQuery : IRequest<JobQueryModel>
    {
        public int JobId { get; set; }
    }
}
