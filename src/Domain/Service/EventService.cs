using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CallCenterAgentManager.Domain.Service
{
    public class EventService<TEvent, TId> : ServiceBase<TEvent, TId>, IEventService<TEvent, TId>
        where TEvent : EventBase<TId>
    {
        public EventService(StrategyFactory strategyFactory) : base(strategyFactory.GetStrategy<TEvent, TId>())
        {
        }

        public BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request)
        {
            var eventEntity = _dataStrategy.ProcessEvent(request);

            var eventResponse = new EventResponse
            {
                EventId = (eventEntity.EventId is Guid ? (Guid)(object)eventEntity.EventId : Guid.Parse(eventEntity.EventId.ToString())),
                AgentId = Guid.Parse(eventEntity.AgentId.ToString()),
                Action = eventEntity.Action,
                TimestampUtc = eventEntity.TimestampUtc,
                QueueIds = request.QueueIds
            };


            return new BaseResponse<EventResponse> { Data = eventResponse };
        }

        public BaseResponse<IEnumerable<EventResponse>> GetRecentEvents()
        {
            var events = _dataStrategy.GetAll()
                                      .OrderByDescending(e => e.TimestampUtc)
                                      .Take(100);

            var eventResponses = new List<EventResponse>();

            foreach (var ev in events)
            {
                eventResponses.Add(new EventResponse
                {
                    EventId = (ev.Id is Guid ? (Guid)(object)ev.Id : Guid.Parse(ev.Id.ToString())),
                    AgentId = Guid.Parse(ev.AgentId.ToString()),
                    Action = ev.Action,
                    TimestampUtc = ev.TimestampUtc
                });
            }

            return new BaseResponse<IEnumerable<EventResponse>> { Data = eventResponses };
        }

    }
}
