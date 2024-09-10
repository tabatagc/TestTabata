using CallCenterAgentManager.Domain.Entities;

namespace CallCenterAgentManager.Domain.Repository
{
    public interface IRepositoryFactory
    {
        IRepositoryBase<TEntity, TId> GetRepository<TEntity,TId>() where TEntity : BaseEntity<TId>;
    }
}
