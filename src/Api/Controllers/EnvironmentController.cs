using CallCenterAgentManager.CrossCutting.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CallCenterAgentManager.Api.Controllers
{
    public class EnvironmentController : Controller
    {
        private readonly IHostEnvironment _env;

        public EnvironmentController(IHostEnvironment env)
        {
            _env = env;
        }

        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Swagger()
        {
            return Redirect($"/swagger/{Settings.SwaggerAPIVersion}/swagger.json");
        }

        [HttpGet]
        [Route("/environment")]
        public IActionResult Index()
        {
            return Json(new
            {
                Environment = _env.EnvironmentName,
                Swagger = new
                {
                    Settings.SwaggerAPIName,
                    Settings.SwaggerAPIVersion
                }
            });
        }
    }
}
