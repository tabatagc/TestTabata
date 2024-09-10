using CallCenterAgentManager.Data.Context.Document;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CallCenterAgentManager.Data.Repositories
{
    public class DocumentRepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public DocumentRepositoryBase(DocumentDbContext context, string collectionName)
        {
            _collection = context.GetCollection<TEntity>(collectionName);
        }
        public TEntity GetById(TId id)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            return _collection.Find(filter).FirstOrDefault();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _collection.AsQueryable().ToList();
        }

        public void Add(TEntity entity)
        {
            entity.CreationDate = DateTime.UtcNow;
            _collection.InsertOne(entity);
        }

        public void Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            _collection.ReplaceOne(filter, entity);
        }

        public void Remove(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            _collection.DeleteOne(filter);
        }

        public void AddOrUpdate(TEntity entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
                Update(entity);
            else
                Add(entity);
        }

        protected IEnumerable<TEntity> FindByFilter(FilterDefinition<TEntity> filter)
        {
            return _collection.Find(filter).ToList();
        }

    }
}
