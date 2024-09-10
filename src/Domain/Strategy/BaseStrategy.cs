using CallCenterAgentManager.Domain.DTO.Request;
using CallCenterAgentManager.Domain.DTO.Response;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Repository;
using CallCenterAgentManager.Domain.Strategy.Contracts;
using System;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Strategy
{
    public abstract class BaseStrategy<TEntity, TId> : IDataStrategy<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        protected readonly IRepositoryFactory _repositoryFactory;

        protected BaseStrategy(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public abstract TEntity GetById(TId id);

        public void Add(TEntity entity)
        {
            var repository = _repositoryFactory.GetRepository<TEntity, TId>();
            repository.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var repository = _repositoryFactory.GetRepository<TEntity, TId>();
            repository.Update(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            var repository = _repositoryFactory.GetRepository<TEntity, TId>();
            repository.AddOrUpdate(entity);
        }

        public void Remove(TEntity entity)
        {
            var repository = _repositoryFactory.GetRepository<TEntity, TId>();
            repository.Remove(entity);
        }
        public IEnumerable<TEntity> GetAll()
        {
            var repository = _repositoryFactory.GetRepository<TEntity, TId>();
            return repository.GetAll();  
        }


        public abstract bool UpdateAgentState(Guid agentId, UpdateAgentStateRequest request);
        public abstract EventResponse ProcessEvent(CallCenterEventRequest request);
        public abstract IEnumerable<QueueResponse> GetQueuesByAgentId(Guid agentId);
    }
}

