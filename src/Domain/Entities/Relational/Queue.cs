using CallCenterAgentManager.Domain.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.Entities.Relational
{
    public class Queue : QueueBase<Guid>, IQueue
    {
        [Required]
        [MaxLength(100)]
        public string QueueName { get; set; }

        public ICollection<Agent> Agents { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
