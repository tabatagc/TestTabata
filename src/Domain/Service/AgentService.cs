using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using System;

namespace CallCenterAgentManager.Domain.Service
{
    public class AgentService : ServiceBase<BaseEntity<object>, object>, IAgentService
    {
        public AgentService(StrategyFactory strategyFactory)
            : base(strategyFactory)
        {
        }

        public BaseResponse<AgentResponse> GetAgentById(Guid agentId)
        {
            object id = Settings.UseNoSqlDatabase ? agentId.ToString() : (object)agentId;
            var agent = _dataStrategy.GetById(id);

            var agentResponse = new AgentResponse
            {
                AgentId = agentId,
                AgentName = (string)(agent.GetType().GetProperty("AgentName").GetValue(agent)),
                Email = (string)(agent.GetType().GetProperty("Email").GetValue(agent)),
                CurrentState = (string)(agent.GetType().GetProperty("State").GetValue(agent)),
                LastActivityTimestampUtc = (DateTime)(agent.GetType().GetProperty("LastActivityTimestampUtc").GetValue(agent))
            };

            return new BaseResponse<AgentResponse> { Data = agentResponse };
        }

        public BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request)
        {
            object id = Settings.UseNoSqlDatabase ? agentId.ToString() : (object)agentId;
            var result = _dataStrategy.UpdateAgentState(id, request);
            return new BaseResponse<bool> { Data = result };
        }
    }
}
