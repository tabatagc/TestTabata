using CallCenterAgentManager.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CallCenterAgentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IAdminApplication _adminApplication;

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration, IAdminApplication adminApplication) : base(logger)
        {
            _configuration = configuration;
            _adminApplication = adminApplication;
        }

        [HttpGet("current-database")]
        public IActionResult GetCurrentDatabase()
        {
            var result = _adminApplication.GetCurrentDatabase();
            return BaseResponse(result);
        }

        [HttpPost("switch-database")]
        public IActionResult SwitchDatabase([FromQuery] bool useMongoDb)
        {
            var result = _adminApplication.SwitchDatabase(useMongoDb);
            return BaseResponse(result);
        }

        [HttpGet("health-check")]
        public IActionResult GetSystemHealth()
        {
            var result = _adminApplication.GetSystemHealth();
            return BaseResponse(result);
        }

        [HttpGet("settings")]
        public IActionResult GetSettings()
        {
            var result = _adminApplication.GetSettings();
            return BaseResponse(result);
        }
    }
}
