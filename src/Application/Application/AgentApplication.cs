using CallCenterAgentManager.Application.AutoMapper.Factory.Contracts;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using Microsoft.Extensions.Logging;

namespace CallCenterAgentManager.Application
{
    public class AgentApplication : ApplicationBase<AgentBase<Guid>, Guid>, IAgentApplication
    {
        private readonly IAgentService<AgentBase<Guid>, Guid> _agentService;
        private readonly ILogger<AgentApplication> _logger;
        private readonly IEntityStrategyFactory _entityStrategyFactory;

        public AgentApplication(
            ILogger<AgentApplication> logger,
            IServiceBase<AgentBase<Guid>, Guid> serviceBase,
            IAgentService<AgentBase<Guid>, Guid> agentService,
            IEntityStrategyFactory entityStrategyFactory)
            : base(entityStrategyFactory)
        {
            _logger = logger;
            _agentService = agentService;
            _entityStrategyFactory = entityStrategyFactory;
        }

        public BaseResponse<AgentResponse> GetAgentById(Guid agentId)
        {
            try
            {
                var strategy = _entityStrategyFactory.GetStrategy<AgentBase<Guid>, Guid>();
                var agent = strategy.GetById(agentId);

                var agentResponse = _entityStrategyFactory.MapEntityToResponse<AgentResponse>(agent);
                return new BaseResponse<AgentResponse> { Data = agentResponse };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<AgentResponse>("Error fetching agent by ID", ex);
            }
        }

        public BaseResponse<AgentResponse> CreateAgent(AgentCreateRequest request)
        {
            try
            {
                var strategy = _entityStrategyFactory.GetStrategy<AgentBase<Guid>, Guid>();
                if (strategy == null)
                {
                    throw new Exception("Strategy is null");
                }

                var newAgent = _entityStrategyFactory.MapRequestToEntity<AgentBase<Guid>>(request);
                if (newAgent == null)
                {
                    throw new Exception("Failed to map request to AgentBase<Guid>");
                }
                
                newAgent.State = "AVAILABLE";
                newAgent.LastActivityTimestampUtc = DateTime.UtcNow;

                strategy.Add(newAgent);

                var agentResponse = _entityStrategyFactory.MapEntityToResponse<AgentResponse>(newAgent);
                return new BaseResponse<AgentResponse> { Data = agentResponse };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<AgentResponse>("Error creating agent", ex);
            }
        }

        public BaseResponse<bool> UpdateAgent(Guid agentId, AgentUpdateRequest request)
        {
            try
            {
                var strategy = _entityStrategyFactory.GetStrategy<AgentBase<Guid>, Guid>();
                var agent = strategy.GetById(agentId);
                if (agent == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Agent not found." } } };
                }

                _entityStrategyFactory.MapRequestToEntity(request, agent);
                strategy.Update(agent);
                return new BaseResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<bool>("Error updating agent", ex);
            }
        }

        public BaseResponse<bool> DeleteAgent(Guid agentId)
        {
            try
            {
                var strategy = _entityStrategyFactory.GetStrategy<AgentBase<Guid>, Guid>();
                var agent = strategy.GetById(agentId);
                if (agent == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Agent not found." } } };
                }

                strategy.Remove(agent);
                return new BaseResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<bool>("Error deleting agent", ex);
            }
        }

        public BaseResponse<AgentResponse> GetAgentState(Guid agentId)
        {
            try
            {
                var strategy = _entityStrategyFactory.GetStrategy<AgentBase<Guid>, Guid>();
                var agent = strategy.GetById(agentId);
                if (agent == null)
                {
                    return new BaseResponse<AgentResponse> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Agent not found." } } };
                }

                var agentResponse = _entityStrategyFactory.MapEntityToResponse<AgentResponse>(agent);
                return new BaseResponse<AgentResponse> { Data = agentResponse };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<AgentResponse>("Error fetching agent state", ex);
            }
        }

        public BaseResponse<bool> UpdateAgentState(Guid agentId, UpdateAgentStateRequest request)
        {
            try
            {
                var result = _agentService.UpdateAgentState(agentId, request);
                return new BaseResponse<bool> { Data = result.Data };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<bool>("Error updating agent state", ex);
            }
        }

        private BaseResponse<T> LogAndReturnError<T>(string message, Exception ex)
        {
            _logger.LogError(ex, message);
            return new BaseResponse<T> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = message } } };
        }
    }
}
