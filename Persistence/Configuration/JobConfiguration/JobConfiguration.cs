using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration.JobConfiguration
{
    public class JobConfiguration : IEntityTypeConfiguration<Domain.Entities.Jobs.Job>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Jobs.Job> builder)
        {
            builder.ToTable(nameof(Job));
            builder.HasKey(k => k.Id);
        }
    }
}
