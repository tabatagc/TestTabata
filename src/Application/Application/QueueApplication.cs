using AutoMapper;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using Microsoft.Extensions.Logging;

namespace CallCenterAgentManager.Application
{
    public class QueueApplication : ApplicationBase<QueueBase<Guid>, Guid>, IQueueApplication
    {
        private readonly IServiceBase<QueueBase<Guid>, Guid> _serviceBase;
        private readonly IMapper _mapper;
        private readonly ILogger<QueueApplication> _logger;

        public QueueApplication(
            IMapper mapper,
            ILogger<QueueApplication> logger,
            IServiceBase<QueueBase<Guid>, Guid> serviceBase)
            : base(serviceBase)
        {
            _mapper = mapper;
            _logger = logger;
            _serviceBase = serviceBase;
        }

        public BaseResponse<QueueResponse> GetQueueById(Guid queueId)
        {
            try
            {
                var queue = GetById(queueId);
                var queueResponse = _mapper.Map<QueueResponse>(queue);
                return new BaseResponse<QueueResponse> { Data = queueResponse };
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
                var queues = _serviceBase.GetAll();
                var queueResponses = _mapper.Map<IEnumerable<QueueResponse>>(queues);

                return new BaseResponse<IEnumerable<QueueResponse>> { Data = queueResponses };
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
                var newQueue = _mapper.Map<QueueBase<Guid>>(request);
                Add(newQueue);
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
                var queue = GetById(queueId);
                if (queue == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Queue not found." } } };
                }

                _mapper.Map(request, queue);
                Update(queue);
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
                var queue = GetById(queueId);
                if (queue == null)
                {
                    return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Queue not found." } } };
                }

                Remove(queue);
                return new BaseResponse<bool> { Data = true };
            }
            catch (Exception ex)
            {
                return LogAndReturnError<bool>("Error deleting queue", ex);
            }
        }

        public IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId)
        {
            return _serviceBase.GetQueuesByAgentId(agentId);
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
