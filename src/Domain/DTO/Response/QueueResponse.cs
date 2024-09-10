using System;

namespace CallCenterAgentManager.Domain.DTO.Response
{
    public class QueueResponse
    {
        public Guid QueueId { get; set; }
        public string QueueName { get; set; }
    }
}
