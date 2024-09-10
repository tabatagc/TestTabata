using CallCenterAgentManager.Domain.Entities;
using System.Collections.Generic;

namespace CallCenterAgentManager.Domain.Repository
{
    public interface IRepositoryBase<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        TEntity GetById(TId id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void AddOrUpdate(TEntity entity);
    }
}
