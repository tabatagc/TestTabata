using CallCenterAgentManager.Domain.Entities.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Entities.Document
{
    public class Agent : BaseEntity<string>, IAgent
    {
        [BsonElement("agentName")]
        public string AgentName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("lastActivityTimestampUtc")]
        public DateTime LastActivityTimestampUtc { get; set; }

        [BsonElement("eventIds")]
        public List<string> EventIds { get; set; } = new List<string>();

        [BsonElement("queueIds")]
        public List<string> QueueIds { get; set; } = new List<string>();
    }
}
