using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Domain.DTO.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CallCenterAgentManager.Application
{
    public class AdminApplication : IAdminApplication
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminApplication> _logger;

        public AdminApplication(IConfiguration configuration, ILogger<AdminApplication> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public BaseResponse<string> GetCurrentDatabase()
        {
            var useMongoDb = Settings.UseNoSqlDatabase;
            var currentDatabase = useMongoDb ? "MongoDB" : "PostgreSQL";
            return new BaseResponse<string> { Data = currentDatabase };
        }

        public BaseResponse<string> SwitchDatabase(bool useMongoDb)
        {
            Settings.UseNoSqlDatabase = useMongoDb;
            var newDatabase = useMongoDb ? "MongoDB" : "PostgreSQL";
            return new BaseResponse<string> { Data = $"Database switched to {newDatabase}. (Changes take effect after a restart)" };
        }

        public BaseResponse<string> GetSystemHealth()
        {
            return new BaseResponse<string> { Data = "System is healthy" };
        }

        public BaseResponse<object> GetSettings()
        {
            return new BaseResponse<object>
            {
                Data = new
                {
                    SwaggerAPIName = Settings.SwaggerAPIName,
                    SwaggerAPIVersion = Settings.SwaggerAPIVersion,
                }
            };
        }
    }
}
