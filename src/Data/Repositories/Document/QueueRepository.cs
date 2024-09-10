using CallCenterAgentManager.Data.Context.Document;
using CallCenterAgentManager.Domain.Entities.Document;
using CallCenterAgentManager.Domain.Repository.Document;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CallCenterAgentManager.Data.Repositories.Document
{
    public class QueueRepository : DocumentRepositoryBase<Queue, string>, IQueueRepository
    {
        public QueueRepository(DocumentDbContext context) : base(context, "Queues")
        {
        }

        public IEnumerable<Queue> GetQueuesByAgentId(string agentId)
        {
            var filter = Builders<Queue>.Filter.AnyEq(q => q.AgentIds, agentId);
            return FindByFilter(filter);
        }
    }
}
