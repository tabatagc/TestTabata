using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IAgentApplication : IApplicationBase<AgentBase<Guid>, Guid>
    {
        BaseResponse<AgentResponse> GetAgentById(Guid agentId);
        BaseResponse<AgentResponse> CreateAgent(AgentCreateRequest request);
        BaseResponse<bool> UpdateAgent(Guid agentId, AgentUpdateRequest request);
        BaseResponse<bool> DeleteAgent(Guid agentId);
        BaseResponse<AgentResponse> GetAgentState(Guid agentId); 
        BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request);
    }
}
