using System;

namespace CallCenterAgentManager.Domain.Entities
{
    public class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}