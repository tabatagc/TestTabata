using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IQueueService<TQueue, TId> : IServiceBase<TQueue, TId> where TQueue : QueueBase<TId>
    {
        BaseResponse<IEnumerable<QueueResponse>> GetQueuesByAgentId(Guid agentId);
    }
}
