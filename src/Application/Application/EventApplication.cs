using CallCenterAgentManager.Application.AutoMapper.Factory.Contracts;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using Microsoft.Extensions.Logging;

namespace CallCenterAgentManager.Application
{
    public class EventApplication : ApplicationBase<EventBase<Guid>, Guid>, IEventApplication
    {
        private readonly IEventService<EventBase<Guid>, Guid> _eventService;
        private readonly ILogger<EventApplication> _logger;
        private readonly IEntityStrategyFactory _entityStrategyFactory;

        public EventApplication(
            ILogger<EventApplication> logger,
            IServiceBase<EventBase<Guid>, Guid> serviceBase,
            IEventService<EventBase<Guid>, Guid> eventService,
            IEntityStrategyFactory entityStrategyFactory)
            : base(entityStrategyFactory)
        {
            _logger = logger;
            _eventService = eventService;
            _entityStrategyFactory = entityStrategyFactory;
        }

        public BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request)
        {
            try
            {
                var strategy = _entityStrategyFactory.GetStrategy<EventBase<Guid>, Guid>();
                var eventEntity = _entityStrategyFactory.MapRequestToEntity<EventBase<Guid>>(request);
                _eventService.ProcessEvent(request);
                var eventResponse = _entityStrategyFactory.MapEntityToResponse<EventResponse>(eventEntity);

                return new BaseResponse<EventResponse> { Data = eventResponse };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<EventResponse>("Error processing event", ex);
            }
        }

        public BaseResponse<IEnumerable<EventResponse>> GetRecentEvents()
        {
            try
            {
                var strategy = _entityStrategyFactory.GetStrategy<EventBase<Guid>, Guid>();
                var recentEvents = strategy.GetAll();
                var eventResponses = _entityStrategyFactory.MapEntityToResponse<IEnumerable<EventResponse>>(recentEvents);

                return new BaseResponse<IEnumerable<EventResponse>> { Data = eventResponses };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<IEnumerable<EventResponse>>("Error fetching recent events", ex);
            }
        }

        private BaseResponse<T> LogAndReturnError<T>(string message, Exception ex)
        {
            _logger.LogError(ex, message);
            return new BaseResponse<T> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = message } } };
        }
    }
}
