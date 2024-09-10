using CallCenterAgentManager.Data.Context.Relational;
using CallCenterAgentManager.Domain.Entities.Relational;
using CallCenterAgentManager.Domain.Repository.Relational;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CallCenterAgentManager.Data.Repositories.Relational
{
    public class QueueRepository : RelationalRepositoryBase<Queue, Guid>, IQueueRepository
    {
        public QueueRepository(RelationalDbContext context) : base(context)
        {
        }

        public IEnumerable<Queue> GetQueuesByAgentId(Guid agentId)
        {
            return _context.Queues
                           .Where(q => q.Agents.Any(a => a.Id == agentId))
                           .ToList();
        }
    }

}
