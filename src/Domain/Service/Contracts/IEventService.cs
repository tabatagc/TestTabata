using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IEventService<TEvent, TId>
    {
        BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request);
        BaseResponse<IEnumerable<EventResponse>> GetRecentEvents();
    }
}
