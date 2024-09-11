using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IEventService<TEvent, TId> : IServiceBase<TEvent, TId> where TEvent : EventBase<TId>
    {
        BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request);
        BaseResponse<IEnumerable<EventResponse>> GetRecentEvents();
    }
}
