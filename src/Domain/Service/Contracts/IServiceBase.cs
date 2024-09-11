using CallCenterAgentManager.Domain.Entities;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Service.Contracts
{
    public interface IServiceBase<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        TEntity GetById(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddOrUpdate(TEntity entity);
        void Remove(TEntity entity);
        IEnumerable<TEntity> GetAll();
    }

}
