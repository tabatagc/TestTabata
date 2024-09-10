using CallCenterAgentManager.Data.Context.Relational;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CallCenterAgentManager.Data.Repositories
{
    public class RelationalRepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        protected readonly RelationalDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RelationalRepositoryBase(RelationalDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity GetById(TId id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(TEntity entity)
        {
            entity.CreationDate = DateTime.UtcNow;
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void AddOrUpdate(TEntity entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
                Update(entity);
            else
                Add(entity);
        }
    }

}
