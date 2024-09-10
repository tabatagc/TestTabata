using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IAgentApplication : IApplicationBase<AgentBase<Guid>, Guid>
    {
        BaseResponse<AgentResponse> GetAgentById(Guid agentId);
        BaseResponse<IEnumerable<AgentResponse>> GetAllAgents();
        BaseResponse<AgentResponse> CreateAgent(AgentCreateRequest request);
    }
}
