using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<AgentApplication> _logger;

        public AgentApplication(
            IMapper mapper,
            ILogger<AgentApplication> logger,
            IServiceBase<AgentBase<Guid>, Guid> serviceBase,
            IAgentService<AgentBase<Guid>, Guid> agentService)
            : base(serviceBase)
        {
            _mapper = mapper;
            _logger = logger;
            _agentService = agentService;
        }

        public BaseResponse<AgentResponse> GetAgentById(Guid agentId)
        {
            try
            {
                var agent = GetById(agentId);

                var agentResponse = _mapper.Map<AgentResponse>(agent);

                return new BaseResponse<AgentResponse>
                {
                    Data = agentResponse
                };
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
                var newAgent = _mapper.Map<AgentBase<Guid>>(request);
                newAgent.State = "AVAILABLE"; 
                newAgent.LastActivityTimestampUtc = DateTime.UtcNow;

                Add(newAgent);
                var agentResponse = _mapper.Map<AgentResponse>(newAgent);

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
                var agent = GetById(agentId);
                if (agent == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Agent not found." } } };
                }

                _mapper.Map(request, agent);
                Update(agent);
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
                var agent = GetById(agentId);
                if (agent == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Agent not found." } } };
                }

                Remove(agent);
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
                var agent = GetById(agentId);
                if (agent == null)
                {
                    return new BaseResponse<AgentResponse> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Agent not found." } } };
                }

                var agentResponse = _mapper.Map<AgentResponse>(agent);
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
