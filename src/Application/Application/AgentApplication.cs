using AutoMapper;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Application
{
    public class AgentApplication : ApplicationBase<AgentBase<Guid>, Guid>, IAgentApplication
    {
        private readonly IAgentService _agentService;
        private readonly IMapper _mapper;
        private readonly ILogger<AgentApplication> _logger;

        public AgentApplication(
            IAgentService agentService,
            IMapper mapper,
            ILogger<AgentApplication> logger,
            IServiceBase<AgentBase<Guid>, Guid> serviceBase)
            : base(serviceBase)
        {
            _agentService = agentService;
            _mapper = mapper;
            _logger = logger;
        }

        public BaseResponse<AgentResponse> GetAgentById(Guid agentId)
        {
            try
            {
                return new BaseResponse<AgentResponse>
                {
                    Data = _mapper.Map<AgentResponse>(_agentService.GetById(agentId))
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
                var agents = _agentService.GetAll();
                return new BaseResponse<IEnumerable<AgentResponse>>
                {
                    Data = _mapper.Map<IEnumerable<AgentResponse>>(agents)
                };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<IEnumerable<AgentResponse>>("Error fetching agents", ex);
            }
        }

        private BaseResponse<T> LogAndReturnError<T>(string message, Exception ex)
        {
            _logger.LogError(ex, message);
            return new BaseResponse<T>
            {
                Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = message } }
            };
        }
    }
}
