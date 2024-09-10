using System;

namespace CallCenterAgentManager.Domain.Entities.Contracts
{
    public interface IQueue
    {
        string QueueName { get; set; }
    }
}
