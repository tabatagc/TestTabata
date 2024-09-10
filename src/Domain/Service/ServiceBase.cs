using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using CallCenterAgentManager.Domain.Strategy.Contracts;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service
{
    public abstract class ServiceBase<TEntity, TId> : IServiceBase<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        protected readonly IDataStrategy<TEntity, TId> _dataStrategy;

        public ServiceBase(StrategyFactory strategyFactory)
        {
            if (Settings.UseNoSqlDatabase && typeof(TId) == typeof(string))
            {
                _dataStrategy = strategyFactory.GetCurrentStrategy<TEntity, string>() as IDataStrategy<TEntity, TId>;
            }
            else if (!Settings.UseNoSqlDatabase && typeof(TId) == typeof(Guid))
            {
                _dataStrategy = strategyFactory.GetCurrentStrategy<TEntity, Guid>() as IDataStrategy<TEntity, TId>;
            }
        }

        public TEntity GetById(TId id)
        {
            return _dataStrategy.GetById(id);
        }

        public bool UpdateState(TId id, UpdateAgentStateRequest request)
        {
            return _dataStrategy.UpdateAgentState(id, request);
        }

        public EventResponse ProcessEvent(CallCenterEventRequest request)
        {
            return _dataStrategy.ProcessEvent(request);
        }

        public IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId)
        {
            return _dataStrategy.GetQueuesByAgentId(agentId);
        }

        public void Add(TEntity entity)
        {
            _dataStrategy.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dataStrategy.Update(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            _dataStrategy.AddOrUpdate(entity);
        }

        public void Remove(TEntity entity)
        {
            _dataStrategy.Remove(entity);
        }
    }
}
