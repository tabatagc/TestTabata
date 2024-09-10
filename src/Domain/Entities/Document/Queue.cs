using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CallCenterAgentManager.Domain.Entities.Document
{
    public class Queue : BaseEntity<string>
    {
        [BsonElement("queueName")]
        public string QueueName { get; set; }

        [BsonElement("agentIds")]
        public List<string> AgentIds { get; set; } = new List<string>();

        [BsonElement("eventIds")]
        public List<string> EventIds { get; set; } = new List<string>();
    }


}
