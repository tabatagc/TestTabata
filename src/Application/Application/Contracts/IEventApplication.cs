using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using System.Collections.Generic;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IEventApplication
    {
        BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request);
        BaseResponse<IEnumerable<EventResponse>> GetRecentEvents();
    }
}
