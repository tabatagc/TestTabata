using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CallCenterAgentManager.Data.Repositories.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRepositoryBase<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : BaseEntity<TId>
        {
            if (Settings.UseNoSqlDatabase)
            {
                if (typeof(TId) == typeof(string))
                {
                    return _serviceProvider.GetService(typeof(DocumentRepositoryBase<TEntity, TId>)) as IRepositoryBase<TEntity, TId>;
                }
                else
                {
                    throw new InvalidOperationException("MongoDB repositories require string as the ID type.");
                }
            }
            else
            {
                if (typeof(TId) == typeof(Guid))
                {
                    return _serviceProvider.GetService(typeof(RelationalRepositoryBase<TEntity, TId>)) as IRepositoryBase<TEntity, TId>;
                }
                else
                {
                    throw new InvalidOperationException("Relational repositories require Guid as the ID type.");
                }
            }
        }
    }
}
