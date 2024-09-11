using CallCenterAgentManager.Application.AutoMapper.Factory.Contracts;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Strategy.Contracts;

namespace CallCenterAgentManager.Application
{
    public class ApplicationBase<TEntity, TId> : IApplicationBase<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        private readonly IDataStrategy<TEntity, TId> _dataStrategy;
        private readonly IEntityStrategyFactory _strategyFactory;

        public ApplicationBase(IEntityStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
            _dataStrategy = strategyFactory.GetStrategy<TEntity, TId>();
        }

        public void Add(object request)
        {
            var entity = _strategyFactory.MapRequestToEntity<TEntity>(request);
            _dataStrategy.Add(entity);
        }

        public TResponse GetById<TResponse>(TId id)
        {
            var entity = _dataStrategy.GetById(id);
            return _strategyFactory.MapEntityToResponse<TResponse>(entity);
        }

        public IEnumerable<TResponse> GetAll<TResponse>()
        {
            var entities = _dataStrategy.GetAll();
            return entities.Select(entity => _strategyFactory.MapEntityToResponse<TResponse>(entity));
        }

        public void Update(TId id, object request)
        {
            var entity = _dataStrategy.GetById(id);
            _strategyFactory.MapRequestToEntity(request, entity);
            _dataStrategy.Update(entity);
        }

        public void Remove(TId id)
        {
            var entity = _dataStrategy.GetById(id);
            _dataStrategy.Remove(entity);
        }
    }
}
