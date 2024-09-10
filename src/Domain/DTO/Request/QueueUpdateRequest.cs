using System;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.DTO.Request
{
    public class QueueUpdateRequest
    {
        [Required]
        public Guid QueueId { get; set; }

        [MaxLength(100)]
        public string QueueName { get; set; }
    }
}
