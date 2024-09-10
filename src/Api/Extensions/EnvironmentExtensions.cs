using Microsoft.Extensions.Hosting;

namespace CallCenterAgentManager.Api.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.EnvironmentName == "Local";
        }
    }
}
