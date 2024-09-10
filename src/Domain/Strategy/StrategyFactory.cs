using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Strategy.Contracts;
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
            if (Settings.UseNoSqlDatabase)
            {
                if (typeof(TId) == typeof(string))
                {
                    if (typeof(TEntity).IsSubclassOf(typeof(BaseEntity<string>)))
                    {
                        var strategyType = typeof(DocumentStrategy<>).MakeGenericType(typeof(TEntity));
                        return _serviceProvider.GetService(strategyType) as IDataStrategy<TEntity, TId>;
                    }
                    else
                    {
                        throw new InvalidOperationException("TEntity must inherit from BaseEntity<string> for DocumentStrategy.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("DocumentStrategy requires string as the ID type.");
                }
            }
            else
            {
                if (typeof(TId) == typeof(Guid))
                {
                    if (typeof(TEntity).IsSubclassOf(typeof(BaseEntity<Guid>)))
                    {
                        var strategyType = typeof(RelationalStrategy<>).MakeGenericType(typeof(TEntity));
                        return _serviceProvider.GetService(strategyType) as IDataStrategy<TEntity, TId>;
                    }
                    else
                    {
                        throw new InvalidOperationException("TEntity must inherit from BaseEntity<Guid> for RelationalStrategy.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("RelationalStrategy requires Guid as the ID type.");
                }
            }
        }
    }
}
