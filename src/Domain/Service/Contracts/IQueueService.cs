using CallCenterAgentManager.Domain.DTO.Response;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IQueueService
    {
        BaseResponse<IEnumerable<QueueResponse>> GetQueuesByAgentId(Guid agentId);
    }
}
