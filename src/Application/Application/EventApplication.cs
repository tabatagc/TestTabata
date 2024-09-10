using AutoMapper;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Service.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Application
{
    public class EventApplication : IEventApplication
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly ILogger<EventApplication> _logger;

        public EventApplication(
            IEventService eventService,
            IMapper mapper,
            ILogger<EventApplication> logger)
        {
            _eventService = eventService;
            _mapper = mapper;
            _logger = logger;
        }

        public BaseResponse<EventResponse> ProcessEvent(CallCenterEventRequest request)
        {
            try
            {
                return _eventService.ProcessEvent(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing event: {ex.Message}");
                return new BaseResponse<EventResponse>
                {
                    Errors = new[] { new ErrorResponse { ErrorMessage = "Error processing event." } }
                };
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
                _logger.LogError($"Error fetching recent events: {ex.Message}");
                return new BaseResponse<IEnumerable<EventResponse>>
                {
                    Errors = new[] { new ErrorResponse { ErrorMessage = "Error fetching recent events." } }
                };
            }
        }
    }
}
