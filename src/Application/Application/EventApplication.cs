using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<EventApplication> _logger;

        public EventApplication(
            IMapper mapper,
            ILogger<EventApplication> logger,
            IServiceBase<EventBase<Guid>, Guid> serviceBase,
            IEventService<EventBase<Guid>, Guid> eventService)
            : base(serviceBase)
        {
            _mapper = mapper;
            _logger = logger;
            _eventService = eventService;
        }

        public BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request)
        {
            try
            {
                return _eventService.ProcessEvent(request);
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
                return _eventService.GetRecentEvents();
            }
            catch (Exception ex)
            {
                return LogAndReturnError<IEnumerable<EventResponse>>("Error fetching recent events", ex);
            }
        }

        private BaseResponse<T> LogAndReturnError<T>(string message, Exception ex)
        {
            _logger.LogError(ex, message);
            return new BaseResponse<T>
            {
                Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = message } }
            };
        }
    }
}
