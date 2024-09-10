using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CallCenterAgentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : BaseController
    {
        private readonly IEventApplication _eventApplication;

        public EventController(ILogger<EventController> logger, IEventApplication eventApplication)
            : base(logger)
        {
            _eventApplication = eventApplication;
        }

        [HttpPost]
        public IActionResult PostEvent([FromBody] CallCenterEventRequest request)
        {
            var result = _eventApplication.ProcessEvent(request);
            return BaseResponse(result);
        }

        [HttpGet("recent")]
        public IActionResult GetRecentEvents()
        {
            var result = _eventApplication.GetRecentEvents();
            return BaseResponse(result);
        }
    }
}
