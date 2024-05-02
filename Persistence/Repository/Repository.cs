using Domain.Entities;
using Domain.Entities.Jobs;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class Repository
    {
        protected readonly AppDbContext DbContext;

        public Repository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async ValueTask<IEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await DbContext.Job.FindAsync(ids, cancellationToken: cancellationToken);
        }
    }
}
