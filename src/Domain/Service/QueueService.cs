using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service
{
    public class QueueService : ServiceBase<BaseEntity<Guid>, Guid>, IQueueService
    {
        public QueueService(StrategyFactory strategyFactory)
            : base(strategyFactory)
        {
        }

        public BaseResponse<IEnumerable<QueueResponse>> GetQueuesByAgentId(Guid agentId)
        {
            var queues = _dataStrategy.GetQueuesByAgentId(agentId);

            var queueResponses = new List<QueueResponse>();
            foreach (var queue in queues)
            {
                queueResponses.Add(new QueueResponse
                {
                    QueueId = queue.Id,
                    QueueName = queue.QueueName
                });
            }

            return new BaseResponse<IEnumerable<QueueResponse>> { Data = queueResponses };
        }
    }
}
