using System;

namespace CallCenterAgentManager.Domain.Entities.Contracts
{
    public interface IAgent
    {
        string AgentName { get; set; }
        string Email { get; set; }
        string State { get; set; }
        DateTime LastActivityTimestampUtc { get; set; }
    }

}
