using System;

namespace CallCenterAgentManager.Domain.DTO.Response
{
    public class AgentResponse
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; }
        public string Email { get; set; }
        public string CurrentState { get; set; }  // ON_CALL, ON_LUNCH, etc.
        public DateTime LastActivityTimestampUtc { get; set; }
    }
}
