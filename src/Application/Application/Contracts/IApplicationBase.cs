using CallCenterAgentManager.Domain.Entities;

namespace CallCenterAgentManager.Application.Contracts
{
    public interface IApplicationBase<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        void Add(object request);
        TResponse GetById<TResponse>(TId id); 
        IEnumerable<TResponse> GetAll<TResponse>();
        void Update(TId id, object request);
        void Remove(TId id);
    }
}
