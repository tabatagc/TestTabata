using System;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.DTO.Request
{
    public class AgentUpdateRequest
    {
        [Required]
        public Guid AgentId { get; set; }

        [MaxLength(50)]
        public string AgentName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
    }
}
