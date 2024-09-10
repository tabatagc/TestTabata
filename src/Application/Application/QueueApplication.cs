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
    public class QueueApplication : IQueueApplication
    {
        private readonly IQueueService _queueService;
        private readonly IMapper _mapper;
        private readonly ILogger<QueueApplication> _logger;

        public QueueApplication(IQueueService queueService, IMapper mapper, ILogger<QueueApplication> logger)
        {
            _queueService = queueService;
            _mapper = mapper;
            _logger = logger;
        }

        public BaseResponse<QueueResponse> GetQueueById(Guid queueId)
        {
            try
            {
                var queue = _queueService.GetById(queueId);
                return new BaseResponse<QueueResponse> { Data = _mapper.Map<QueueResponse>(queue) };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<QueueResponse>("Error fetching queue by ID", ex);
            }
        }

        public BaseResponse<IEnumerable<QueueResponse>> GetAllQueues()
        {
            try
            {
                var queues = _queueService.GetAll();
                return new BaseResponse<IEnumerable<QueueResponse>> { Data = _mapper.Map<IEnumerable<QueueResponse>>(queues) };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<IEnumerable<QueueResponse>>("Error fetching all queues", ex);
            }
        }

        public BaseResponse<bool> CreateQueue(QueueCreateRequest request)
        {
            try
            {
                _queueService.Add(_mapper.Map<Queue>(request));
                return new BaseResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<bool>("Error creating queue", ex);
            }
        }

        public BaseResponse<bool> UpdateQueue(Guid queueId, QueueUpdateRequest request)
        {
            try
            {
                var queue = _queueService.GetById(queueId);
                if (queue == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Queue not found." } } };
                }

                _mapper.Map(request, queue);
                _queueService.Update(queue);
                return new BaseResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<bool>("Error updating queue", ex);
            }
        }

        public BaseResponse<bool> DeleteQueue(Guid queueId)
        {
            try
            {
                var queue = _queueService.GetById(queueId);
                if (queue == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Queue not found." } } };
                }

                _queueService.Remove(queue);
                return new BaseResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<bool>("Error deleting queue", ex);
            }
        }

        public IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId)
        {
            return _queueService.GetQueuesByAgentId(agentId);
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
