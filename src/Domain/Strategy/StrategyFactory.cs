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

        public IDataStrategy<TEntity, TId> GetCurrentStrategy<TEntity, TId>()
            where TEntity : BaseEntity<TId>
        {
            if (Settings.UseNoSqlDatabase)
            {
                if (typeof(TId) == typeof(string))
                {
                    return (IDataStrategy<TEntity, TId>)_serviceProvider.GetService(typeof(DocumentStrategy<TEntity, TId>));
                }
                else
                {
                    throw new InvalidOperationException("NoSQL database strategy requires string as the ID type.");
                }
            }
            else
            {
                if (typeof(TId) == typeof(Guid))
                {
                    return (IDataStrategy<TEntity, TId>)_serviceProvider.GetService(typeof(RelationalStrategy<TEntity, TId>));
                }
                else
                {
                    throw new InvalidOperationException("Relational database strategy requires Guid as the ID type.");
                }
            }
        }
    }

}
