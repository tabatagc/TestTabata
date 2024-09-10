using System;
using System.Collections.Generic;
using CallCenterAgentManager.Domain.Entities.Relational;

namespace CallCenterAgentManager.Domain.Repository.Relational
{
    public interface IQueueRepository : IRepositoryBase<Queue, Guid>
    {
        IEnumerable<Queue> GetQueuesByAgentId(Guid agentId);
    }

}
