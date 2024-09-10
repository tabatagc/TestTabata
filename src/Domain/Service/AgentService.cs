using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Strategy;
using System;

namespace CallCenterAgentManager.Domain.Service
{
    public class AgentService<TAgent, TId> : ServiceBase<TAgent, TId> where TAgent : AgentBase<TId> 
    {
        public AgentService(StrategyFactory strategyFactory) : base(strategyFactory.GetStrategy<TAgent, TId>())
        {
        }

        public BaseResponse<AgentResponse> GetAgentById(TId agentId)
        {
            var agent = _dataStrategy.GetById(agentId);

            var agentResponse = new AgentResponse
            {
                AgentId = (agent.Id is Guid ? (Guid)(object)agent.Id : Guid.Parse(agent.Id.ToString())),
                AgentName = agent.AgentName,
                Email = agent.Email,
                CurrentState = agent.State,
                LastActivityTimestampUtc = agent.LastActivityTimestampUtc
            };

            return new BaseResponse<AgentResponse> { Data = agentResponse };
        }

        public BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request)
        {
            var result = _dataStrategy.UpdateAgentState(agentId, request);
            return new BaseResponse<bool> { Data = result };
        }
    }
}
