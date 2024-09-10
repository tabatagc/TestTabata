namespace CallCenterAgentManager.Application.Contracts
{
    public interface IApplicationBase<TEntity>
    {
        TEntity GetById(Guid id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddOrUpdate(TEntity entity);
        void Remove(TEntity entity);
    }
}
