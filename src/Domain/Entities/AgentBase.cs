using System;

namespace CallCenterAgentManager.Domain.Entities
{
    public class AgentBase<TId> : BaseEntity<TId>
    {
        public string AgentName { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public DateTime LastActivityTimestampUtc { get; set; }
    }
}
