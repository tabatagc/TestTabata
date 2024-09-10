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
        private readonly IServiceBase<AgentBase<Guid>, Guid> _serviceBase;
        private readonly IMapper _mapper;
        private readonly ILogger<AgentApplication> _logger;

        public AgentApplication(
            IMapper mapper,
            ILogger<AgentApplication> logger,
            IServiceBase<AgentBase<Guid>, Guid> serviceBase)
            : base(serviceBase)
        {
            _mapper = mapper;
            _logger = logger;
            _serviceBase = serviceBase;
        }

        public BaseResponse<AgentResponse> CreateAgent(AgentCreateRequest request)
        {
            try
            {
                var newAgent = _mapper.Map<AgentBase<Guid>>(request);
                Add(newAgent); 
                var agentResponse = _mapper.Map<AgentResponse>(newAgent);
                return new BaseResponse<AgentResponse> { Data = agentResponse };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<AgentResponse>("Error creating agent", ex);
            }
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

        public BaseResponse<IEnumerable<AgentResponse>> GetAllAgents()
        {
            try
            {
                var agents = _serviceBase.GetAll();

                var agentResponses = _mapper.Map<IEnumerable<AgentResponse>>(agents);

                return new BaseResponse<IEnumerable<AgentResponse>>
                {
                    Data = agentResponses
                };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<IEnumerable<AgentResponse>>("Error fetching all agents", ex);
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
                var agent = GetById(agentId);
                if (agent == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Agent not found." } } };
                }

                agent.State = request.Action;
                Update(agent);
                return new BaseResponse<bool> { Data = true };
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
