using System;

namespace CallCenterAgentManager.Domain.Entities
{
    public class QueueBase<TId> : BaseEntity<TId>
    {
        public string QueueName { get; set; }
    }
}
