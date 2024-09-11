using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using System;

namespace CallCenterAgentManager.Domain.Service
{
    public class AgentService<TAgent, TId> : ServiceBase<TAgent, TId>, IAgentService<TAgent, TId>
    where TAgent : AgentBase<TId>
    {
        public AgentService(StrategyFactory strategyFactory) : base(strategyFactory.GetStrategy<TAgent, TId>())
        {
        }

        public BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request)
        {
            var result = _dataStrategy.UpdateAgentState(agentId, request);
            return new BaseResponse<bool> { Data = result };
        }
    }
}
