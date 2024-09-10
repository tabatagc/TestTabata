using CallCenterAgentManager.Domain.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CallCenterAgentManager.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ILogger<BaseController> _logger { get; }

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected void LogError(Exception ex)
        {
            _logger.LogError(ex, "An error occurred.");
        }

        public IActionResult BaseResponse<T>(BaseResponse<T> response)
        {
            if (response.Errors != null && response.Errors.Count > 0)
            {
                foreach (var error in response.Errors)
                {
                    LogError(new Exception(error.ErrorMessage));
                }

                return BadRequest(response);
            }
            else
            {
                return Ok(response.Data);
            }
        }
    }
}
