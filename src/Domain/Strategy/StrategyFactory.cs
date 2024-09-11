using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Strategy.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CallCenterAgentManager.Domain.Strategy
{
    public class StrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public StrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataStrategy<TEntity, TId> GetStrategy<TEntity, TId>() where TEntity : BaseEntity<TId>
        {
            Type strategyType;

            if (Settings.UseNoSqlDatabase)
            {
                strategyType = typeof(DocumentStrategy<TEntity, TId>);
            }
            else
            {
                strategyType = typeof(RelationalStrategy<TEntity, TId>);
            }
            var strategy = _serviceProvider.GetRequiredService(strategyType);

            if (strategy == null)
            {
                throw new Exception($"Failed to resolve strategy: {strategyType.FullName}");
            }

            return (IDataStrategy<TEntity, TId>)strategy;
        }
    }

}
