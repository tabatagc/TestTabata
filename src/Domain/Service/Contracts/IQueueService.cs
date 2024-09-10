using CallCenterAgentManager.Domain.DTO.Response;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IQueueService<TQueue, TId>
    {
        BaseResponse<IEnumerable<QueueResponse>> GetQueuesByAgentId(Guid agentId);
    }
}
