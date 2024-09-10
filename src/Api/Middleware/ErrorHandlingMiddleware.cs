using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CallCenterAgentManager.Api.Controllers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public ErrorHandlingMiddleware(RequestDelegate next, IOptions<JsonOptions> jsonOptions)
        {
            this.next = next;
            this.jsonSerializerOptions = jsonOptions.Value.JsonSerializerOptions;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An error occurred",
                Detail = exception.Message,
                Instance = context.Request.Path,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status.Value;

            var json = JsonSerializer.Serialize(problemDetails, jsonSerializerOptions);
            await context.Response.WriteAsync(json);
        }
    }
}