using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.DTO.Request
{
    public class CallCenterEventRequest
    {
        [Required]
        public Guid AgentId { get; set; }

        [Required]
        public string AgentName { get; set; }

        [Required]
        public string Action { get; set; }  // Event type, like CALL_STARTED, START_DO_NOT_DISTURB

        [Required]
        public DateTime TimestampUtc { get; set; }

        public IEnumerable<Guid> QueueIds { get; set; }
    }
}
