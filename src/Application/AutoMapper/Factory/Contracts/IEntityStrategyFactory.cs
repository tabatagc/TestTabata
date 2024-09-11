using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Strategy.Contracts;

namespace CallCenterAgentManager.Application.AutoMapper.Factory.Contracts
{
    public interface IEntityStrategyFactory
    {
       IDataStrategy<TEntity, TId> GetStrategy<TEntity, TId>() where TEntity : BaseEntity<TId>;
       TEntity MapRequestToEntity<TEntity>(object request);
       TResponse MapEntityToResponse<TResponse>(object entity);
       void MapRequestToEntity(object request, object entity);

    }
}
