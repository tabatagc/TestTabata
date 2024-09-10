using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.DTO.Request
{
    public class UpdateAgentStateRequest
    {
        [Required]
        public string Action { get; set; }  // CALL_STARTED, START_DO_NOT_DISTURB, etc.

        [Required]
        public DateTime TimestampUtc { get; set; }

        public IEnumerable<Guid> QueueIds { get; set; } // Related queues to sync with
    }
}
