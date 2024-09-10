using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using System;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IAgentService
    {
        BaseResponse<AgentResponse> GetAgentById(Guid agentId);
        BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request);
    }
}


