using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IEventApplication : IApplicationBase<EventBase<Guid>, Guid>
    {
        BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request);
        BaseResponse<IEnumerable<EventResponse>> GetRecentEvents();
    }
}
