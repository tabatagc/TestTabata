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
        private readonly IServiceBase<EventBase<Guid>, Guid> _serviceBase;
        private readonly IMapper _mapper;
        private readonly ILogger<EventApplication> _logger;

        public EventApplication(
            IMapper mapper,
            ILogger<EventApplication> logger,
            IServiceBase<EventBase<Guid>, Guid> serviceBase)
            : base(serviceBase)
        {
            _mapper = mapper;
            _logger = logger;
            _serviceBase = serviceBase;
        }

        public BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request)
        {
            try
            {
                var eventEntity = _mapper.Map<EventBase<Guid>>(request);
                Add(eventEntity);
                var eventResponse = _mapper.Map<EventResponse>(eventEntity);
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
                var events = _serviceBase.GetAll();
                var eventResponses = _mapper.Map<IEnumerable<EventResponse>>(events);

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
            return new BaseResponse<T>
            {
                Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = message } }
            };
        }
    }
}
