using System.Collections.Generic;
using CallCenterAgentManager.Domain.Entities.Document;

namespace CallCenterAgentManager.Domain.Repository.Document
{
    public interface IQueueRepository : IRepositoryBase<Queue, string>
    {
        IEnumerable<Queue> GetQueuesByAgentId(string agentId);
    }

}
