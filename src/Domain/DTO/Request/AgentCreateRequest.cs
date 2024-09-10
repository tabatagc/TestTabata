using System;
using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.DTO.Request
{
    public class AgentCreateRequest
    {
        [Required]
        [MaxLength(50)]
        public string AgentName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public Guid AgentId { get; set; }
    }
}
