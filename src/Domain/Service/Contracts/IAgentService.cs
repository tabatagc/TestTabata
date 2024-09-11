using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using System;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IAgentService<TAgent, TId> : IServiceBase<TAgent, TId> where TAgent : AgentBase<TId>
    {
        BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request);
    }
}


