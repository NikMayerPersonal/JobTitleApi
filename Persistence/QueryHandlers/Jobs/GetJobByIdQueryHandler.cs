using Application.Jobs.Query.GetJobById;
using Domain.Entities.Jobs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.QueryHandlers.Jobs
{
    /// <summary>
    /// A MediatR Handler to Handle GetJobById requests
    /// </summary>
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobQueryModel>
    {
        private readonly AppDbContext _dbContext;

        public GetJobByIdQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<JobQueryModel> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _dbContext.Set<Job>().Where(j => j.Id == request.JobId).Select(j => new JobQueryModel
            {
                Name = j.Name
            }).FirstOrDefaultAsync(cancellationToken);

            return job;
        }
    }
}
