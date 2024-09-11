using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy.Contracts;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service
{
    public class ServiceBase<TEntity, TId> : IServiceBase<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        protected readonly IDataStrategy<TEntity, TId> _dataStrategy;

        public ServiceBase(IDataStrategy<TEntity, TId> dataStrategy)
        {
            _dataStrategy = dataStrategy;
        }

        public virtual TEntity GetById(TId id)
        {
            return _dataStrategy.GetById(id);
        }

        public virtual void Add(TEntity entity)
        {
            _dataStrategy.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dataStrategy.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _dataStrategy.Remove(entity);
        }

        public virtual void AddOrUpdate(TEntity entity)
        {
            _dataStrategy.AddOrUpdate(entity);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dataStrategy.GetAll();
        }
    }
}
