using System;
using System.Collections.Generic;
using CallCenterAgentManager.Domain.Entities.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CallCenterAgentManager.Domain.Entities.Document
{
    public class Event : BaseEntity<string>, IEvent
    {

        [BsonElement("agentId")]
        public string AgentId { get; set; }

        [BsonElement("action")]
        public string Action { get; set; }

        [BsonElement("timestampUtc")]
        public DateTime TimestampUtc { get; set; }

        [BsonElement("queueIds")]
        public List<string> QueueIds { get; set; } = new List<string>();
    }
}
