using AutoMapper;
using CallCenterAgentManager.Application.AutoMapper.Factory.Contracts;
using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Strategy;
using CallCenterAgentManager.Domain.Strategy.Contracts;

namespace CallCenterAgentManager.Application.AutoMapper.Factory
{
    public class EntityStrategyFactory : IEntityStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public EntityStrategyFactory(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public IDataStrategy<TEntity, TId> GetStrategy<TEntity, TId>() where TEntity : BaseEntity<TId>
        {
            var strategyType = Settings.UseNoSqlDatabase
                ? typeof(DocumentStrategy<TEntity, TId>)
                : typeof(RelationalStrategy<TEntity, TId>);

            var strategy = _serviceProvider.GetService(strategyType);

            if (strategy == null)
            {
                throw new Exception($"Failed to resolve strategy: {strategyType.FullName}");
            }

            return (IDataStrategy<TEntity, TId>)strategy;

        }

        public TEntity MapRequestToEntity<TEntity>(object request)
        {
            return _mapper.Map<TEntity>(request);
        }

        public TResponse MapEntityToResponse<TResponse>(object entity)
        {
            return _mapper.Map<TResponse>(entity);
        }

        public void MapRequestToEntity(object request, object entity)
        {
            _mapper.Map(request, entity);
        }
    }
}
