using System.ComponentModel.DataAnnotations;

namespace CallCenterAgentManager.Domain.DTO.Request
{
    public class QueueCreateRequest
    {
        [Required]
        [MaxLength(100)]
        public string QueueName { get; set; }
    }
}
