using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.DTO.Response
{
    public class EventResponse
    {
        public Guid EventId { get; set; }
        public Guid AgentId { get; set; }
        public string AgentName { get; set; }
        public string Action { get; set; }
        public DateTime TimestampUtc { get; set; }
        public IEnumerable<Guid> QueueIds { get; set; }
    }
}
