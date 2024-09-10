using System;

namespace CallCenterAgentManager.Domain.Entities.Contracts
{
    public interface IEvent
    {
        string Action { get; set; }
        DateTime TimestampUtc { get; set; }
    }
}
