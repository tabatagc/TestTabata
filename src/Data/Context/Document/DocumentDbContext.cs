using CallCenterAgentManager.Domain.Entities.Document;
using MongoDB.Driver;

namespace CallCenterAgentManager.Data.Context.Document
{
    public class DocumentDbContext
    {
        private readonly IMongoDatabase _database;

        public DocumentDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
        {
            return _database.GetCollection<TEntity>(collectionName);
        }

        public IMongoCollection<Agent> Agents => _database.GetCollection<Agent>("Agents");
        public IMongoCollection<Event> Events => _database.GetCollection<Event>("Events");
        public IMongoCollection<Queue> Queues => _database.GetCollection<Queue>("Queues");
    }
}
