using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CallCenterAgentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : BaseController
    {
        private readonly IQueueService _queueService;

        public QueueController(ILogger<QueueController> logger, IQueueService queueService)
            : base(logger)
        {
            _queueService = queueService;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetQueueById(Guid id)
        {
            var result = _queueService.GetQueueById(id);
            return BaseResponse(result);
        }

        [HttpGet]
        public IActionResult GetAllQueues()
        {
            var result = _queueService.GetAllQueues();
            return BaseResponse(result);
        }

        [HttpPost]
        public IActionResult CreateQueue([FromBody] QueueCreateRequest request)
        {
            var result = _queueService.CreateQueue(request);
            return BaseResponse(result);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateQueue(Guid id, [FromBody] QueueUpdateRequest request)
        {
            var result = _queueService.UpdateQueue(id, request);
            return BaseResponse(result);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteQueue(Guid id)
        {
            var result = _queueService.DeleteQueue(id);
            return BaseResponse(result);
        }
    }
}
