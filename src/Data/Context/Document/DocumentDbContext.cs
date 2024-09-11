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
    }
}
