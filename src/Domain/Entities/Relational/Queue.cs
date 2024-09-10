using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.Entities.Relational
{
    public class Queue : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(100)]
        public string QueueName { get; set; }

        public ICollection<Agent> Agents { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
