using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CallCenterAgentManager.Domain.Entities.Relational;

namespace CallCenterAgentManager.Data.Mapping.Relational
{
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Action).HasMaxLength(50).IsRequired();
            builder.Property(e => e.TimestampUtc).IsRequired();


            builder.HasOne(e => e.Agent)
                   .WithMany(a => a.Events)
                   .HasForeignKey(e => e.AgentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Queues)
                   .WithMany(q => q.Events)
                   .UsingEntity(j => j.ToTable("EventQueue"));
        }
    }
}
