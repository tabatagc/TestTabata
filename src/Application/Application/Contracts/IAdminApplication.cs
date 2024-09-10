using CallCenterAgentManager.Domain.DTO.Response;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IAdminApplication
    {
        BaseResponse<string> GetCurrentDatabase();
        BaseResponse<string> SwitchDatabase(bool useMongoDb);
        BaseResponse<string> GetSystemHealth();
        BaseResponse<object> GetSettings();
    }
}
