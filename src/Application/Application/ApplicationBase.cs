using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Service.Contracts;

namespace CallCenterAgentManager.Application
{
    public class ApplicationBase<TEntity, TId> : IApplicationBase<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        private readonly IServiceBase<TEntity, TId> _serviceBase;

        public ApplicationBase(IServiceBase<TEntity, TId> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity entity)
        {
            _serviceBase.Add(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            _serviceBase.AddOrUpdate(entity);
        }

        public TEntity GetById(TId id)
        {
            return _serviceBase.GetById(id);
        }

        public void Remove(TEntity entity)
        {
            _serviceBase.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _serviceBase.Update(entity);
        }
    }
}
