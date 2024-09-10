using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CallCenterAgentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController : BaseController
    {
        private readonly IAgentApplication _agentApplication;

        public AgentController(ILogger<AgentController> logger, IAgentApplication agentApplication)
            : base(logger)
        {
            _agentApplication = agentApplication;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetAgentById(Guid id)
        {
            var result = _agentApplication.GetAgentById(id);
            return BaseResponse(result);
        }

        [HttpPost]
        public IActionResult CreateAgent([FromBody] AgentCreateRequest request)
        {
            var result = _agentApplication.CreateAgent(request);
            return BaseResponse(result);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateAgent(Guid id, [FromBody] AgentUpdateRequest request)
        {
            var result = _agentApplication.UpdateAgent(id, request);
            return BaseResponse(result);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteAgent(Guid id)
        {
            var result = _agentApplication.DeleteAgent(id);
            return BaseResponse(result);
        }

        [HttpGet("{id:guid}/state")]
        public IActionResult GetAgentState(Guid id)
        {
            var result = _agentApplication.GetAgentState(id);
            return BaseResponse(result);
        }

        [HttpPut("{id:guid}/state")]
        public IActionResult UpdateAgentState(Guid id, [FromBody] UpdateAgentStateRequest request)
        {
            var result = _agentApplication.UpdateAgentState(id, request);
            return BaseResponse(result);
        }
    }
}
