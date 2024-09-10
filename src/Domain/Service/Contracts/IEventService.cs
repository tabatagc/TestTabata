using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IEventService<TEvent, TId>
    {
        BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request);
    }
}
