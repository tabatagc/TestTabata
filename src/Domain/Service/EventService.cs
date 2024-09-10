using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using System;

namespace CallCenterAgentManager.Domain.Service
{
    public class EventService : ServiceBase<BaseEntity<Guid>, Guid>, IEventService
    {
        public EventService(StrategyFactory strategyFactory)
            : base(strategyFactory)
        {
        }

        public BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request)
        {
            var eventEntity = _dataStrategy.ProcessEvent(request);

            var eventResponse = new EventResponse
            {
                EventId = eventEntity.Id,
                AgentId = eventEntity.AgentId,
                Action = eventEntity.Action,
                TimestampUtc = eventEntity.TimestampUtc
            };

            return new BaseResponse<EventResponse> { Data = eventResponse };
        }
    }
}
