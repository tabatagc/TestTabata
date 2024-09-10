using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Strategy;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service
{
    public class QueueService<TQueue, TId> : ServiceBase<TQueue, TId> where TQueue : QueueBase<TId>
    {
        public QueueService(StrategyFactory strategyFactory) : base(strategyFactory.GetStrategy<TQueue, TId>())
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
                    QueueId = (queue.QueueId is Guid ? (Guid)(object)queue.QueueId : Guid.Parse(queue.QueueId.ToString())),
                    QueueName = queue.QueueName
                });
            }

            return new BaseResponse<IEnumerable<QueueResponse>> { Data = queueResponses };
        }
    }
}
