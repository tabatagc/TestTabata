using CallCenterAgentManager.Domain.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.Entities.Relational
{
    public class Agent : BaseEntity<Guid>, IAgent
    {

        [Required]
        [MaxLength(100)]
        public string AgentName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        public DateTime LastActivityTimestampUtc { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Queue> Queues { get; set; }
    }
}
