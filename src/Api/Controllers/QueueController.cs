using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : BaseController
    {
        private readonly IQueueApplication _queueApplication;

        public QueueController(ILogger<QueueController> logger, IQueueApplication queueApplication)
            : base(logger)
        {
            _queueApplication = queueApplication;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetQueueById(Guid id)
        {
            var result = _queueApplication.GetQueueById(id);
            return BaseResponse<QueueResponse>(result);
        }

        [HttpGet]
        public IActionResult GetAllQueues()
        {
            var result = _queueApplication.GetAllQueues();
            return BaseResponse<IEnumerable<QueueResponse>>(result); 
        }

        [HttpPost]
        public IActionResult CreateQueue([FromBody] QueueCreateRequest request)
        {
            var result = _queueApplication.CreateQueue(request);
            return BaseResponse<bool>(result); 
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateQueue(Guid id, [FromBody] QueueUpdateRequest request)
        {
            var result = _queueApplication.UpdateQueue(id, request);
            return BaseResponse<bool>(result); 
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteQueue(Guid id)
        {
            var result = _queueApplication.DeleteQueue(id);
            return BaseResponse<bool>(result);
        }

        [HttpGet("agent/{agentId:guid}")]
        public IActionResult GetQueuesByAgentId(Guid agentId)
        {
            var result = new BaseResponse<IEnumerable<QueueResponse>>
            {
                Data = _queueApplication.GetQueuesByAgentId(agentId)
            };
            return BaseResponse(result);
        }

    }
}
