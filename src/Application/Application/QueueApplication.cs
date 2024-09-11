using CallCenterAgentManager.Application.AutoMapper.Factory.Contracts;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Application;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using Microsoft.Extensions.Logging;

public class QueueApplication : ApplicationBase<QueueBase<Guid>, Guid>, IQueueApplication
{
    private readonly IQueueService<QueueBase<Guid>, Guid> _queueService;
    private readonly ILogger<QueueApplication> _logger;
    private readonly IEntityStrategyFactory _entityStrategyFactory;

    public QueueApplication(
        ILogger<QueueApplication> logger,
        IServiceBase<QueueBase<Guid>, Guid> serviceBase,
        IQueueService<QueueBase<Guid>, Guid> queueService,
        IEntityStrategyFactory entityStrategyFactory)
        : base(entityStrategyFactory)
    {
        _logger = logger;
        _queueService = queueService;
        _entityStrategyFactory = entityStrategyFactory;
    }

    public BaseResponse<QueueResponse> GetQueueById(Guid queueId)
    {
        try
        {
            var strategy = _entityStrategyFactory.GetStrategy<QueueBase<Guid>, Guid>();
            var queue = strategy.GetById(queueId);
            var queueResponse = _entityStrategyFactory.MapEntityToResponse<QueueResponse>(queue);
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
            var strategy = _entityStrategyFactory.GetStrategy<QueueBase<Guid>, Guid>();
            var queues = strategy.GetAll();
            var queueResponses = _entityStrategyFactory.MapEntityToResponse<IEnumerable<QueueResponse>>(queues);
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
            var strategy = _entityStrategyFactory.GetStrategy<QueueBase<Guid>, Guid>();
            var newQueue = _entityStrategyFactory.MapRequestToEntity<QueueBase<Guid>>(request);
            strategy.Add(newQueue);
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
            var strategy = _entityStrategyFactory.GetStrategy<QueueBase<Guid>, Guid>();
            var queue = strategy.GetById(queueId);
            if (queue == null)
            {
                return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Queue not found." } } };
            }

            _entityStrategyFactory.MapRequestToEntity(request, queue);
            strategy.Update(queue);
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
            var strategy = _entityStrategyFactory.GetStrategy<QueueBase<Guid>, Guid>();
            var queue = strategy.GetById(queueId);
            if (queue == null)
            {
                return new BaseResponse<bool> { Errors = new List<ErrorResponse> { new ErrorResponse { ErrorMessage = "Queue not found." } } };
            }

            strategy.Remove(queue);
            return new BaseResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return LogAndReturnError<bool>("Error deleting queue", ex);
        }
    }

    public IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId)
    {
        return _queueService.GetQueuesByAgentId(agentId).Data;
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
