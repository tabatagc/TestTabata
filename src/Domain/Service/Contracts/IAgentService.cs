using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IAgentService : IServiceBase<AgentBase<Guid>, Guid>
    {
        BaseResponse<AgentResponse> GetAgentById(Guid agentId);
        BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request);
        IEnumerable<AgentBase<Guid>> GetAll();
    }
}


