using Domain.Entities;
using Domain.Entities.Jobs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Domain.Entities.Jobs.Job> Job { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntity).Assembly);
        }
    }
}
