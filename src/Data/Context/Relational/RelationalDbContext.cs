using Microsoft.EntityFrameworkCore;
using CallCenterAgentManager.Domain.Entities.Relational;

namespace CallCenterAgentManager.Data.Context.Relational
{
    public class RelationalDbContext : DbContext
    {
        public RelationalDbContext(DbContextOptions<RelationalDbContext> options) : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Queue> Queues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RelationalDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
