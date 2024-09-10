using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IServiceBase<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        TEntity GetById(TId id);
        bool UpdateState(TId id, UpdateAgentStateRequest request);
        EventResponse ProcessEvent(CallCenterEventRequest request);
        IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddOrUpdate(TEntity entity);
        void Remove(TEntity entity);
        IEnumerable<TEntity> GetAll();
    }

}
