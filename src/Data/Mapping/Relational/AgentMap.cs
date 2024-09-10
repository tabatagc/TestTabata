using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CallCenterAgentManager.Domain.Entities.Relational;

namespace CallCenterAgentManager.Data.Mapping.Relational
{
    public class AgentMap : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.ToTable("Agent");

            builder.HasKey(agent => agent.Id);

            builder.Property(agent => agent.AgentName).HasMaxLength(100).IsRequired();
            builder.Property(agent => agent.Email).HasMaxLength(200).IsRequired();
            builder.Property(agent => agent.State).HasMaxLength(50).IsRequired();
            builder.Property(agent => agent.LastActivityTimestampUtc).IsRequired();

            builder.HasMany(agent => agent.Events)
                   .WithOne(ev => ev.Agent)
                   .HasForeignKey(ev => ev.AgentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(agent => agent.Queues)
                   .WithMany(q => q.Agents)
                   .UsingEntity(j => j.ToTable("AgentQueue"));
        }
    }
}
