using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CallCenterAgentManager.Domain.Entities.Relational;

namespace CallCenterAgentManager.Data.Mapping.Relational
{
    public class QueueMap : IEntityTypeConfiguration<Queue>
    {
        public void Configure(EntityTypeBuilder<Queue> builder)
        {
            builder.ToTable("Queue");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.QueueName).HasMaxLength(100).IsRequired();

            builder.HasMany(q => q.Agents)
                   .WithMany(a => a.Queues)
                   .UsingEntity(j => j.ToTable("AgentQueue"));

            builder.HasMany(q => q.Events)
                   .WithMany(e => e.Queues)
                   .UsingEntity(j => j.ToTable("EventQueue"));
        }
    }
}
