using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.Entities.Relational
{
    public class Event : BaseEntity<Guid>
    {
        [Required]
        public Guid AgentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Action { get; set; }

        [Required]
        public DateTime TimestampUtc { get; set; }

        public Agent Agent { get; set; }
        public ICollection<Queue> Queues { get; set; } = new List<Queue>();
    }
}
