using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IQueueApplication : IApplicationBase<QueueBase<Guid>, Guid>
    {
        BaseResponse<QueueResponse> GetQueueById(Guid queueId);
        BaseResponse<IEnumerable<QueueResponse>> GetAllQueues();
        BaseResponse<bool> CreateQueue(QueueCreateRequest request);
        BaseResponse<bool> UpdateQueue(Guid queueId, QueueUpdateRequest request);
        BaseResponse<bool> DeleteQueue(Guid queueId);
        IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId);
    }
}
