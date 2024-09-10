using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace CallCenterAgentManager.API.Swagger
{
    public class FormDataOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formMediaType = context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<ConsumesAttribute>()
                .SelectMany(attr => attr.ContentTypes)
                .FirstOrDefault(mediaType => mediaType == "application/x-www-form-urlencoded");

            if (formMediaType != null)
            {
                operation.RequestBody.Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        formMediaType, new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>()
                            }
                        }
                    }
                };
            }


        }
    }
}
