using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IAgentApplication
    {
        BaseResponse<AgentResponse> GetAgentById(Guid agentId);
        BaseResponse<AgentResponse> CreateAgent(AgentCreateRequest request);
        BaseResponse<AgentResponse> UpdateAgent(Guid agentId, AgentUpdateRequest request);
        BaseResponse<bool> DeleteAgent(Guid agentId);
        BaseResponse<AgentResponse> GetAgentState(Guid agentId);
        BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request);
        BaseResponse<IEnumerable<AgentResponse>> GetAllAgents();
    }
}
