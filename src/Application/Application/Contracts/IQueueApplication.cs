using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IQueueApplication
    {
        BaseResponse<QueueResponse> GetQueueById(Guid queueId);
        BaseResponse<IEnumerable<QueueResponse>> GetAllQueues();
        BaseResponse<bool> CreateQueue(QueueCreateRequest request);
        BaseResponse<bool> UpdateQueue(Guid queueId, QueueUpdateRequest request);
        BaseResponse<bool> DeleteQueue(Guid queueId);
        IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId);
    }
}
