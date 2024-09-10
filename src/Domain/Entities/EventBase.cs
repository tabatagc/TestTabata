using System;

namespace CallCenterAgentManager.Domain.Entities
{
    public class EventBase<TId> : BaseEntity<TId>
    {
        public string AgentId { get; set; }
        public string Action { get; set; }
        public DateTime TimestampUtc { get; set; }
    }
}

