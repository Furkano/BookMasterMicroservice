using CategoryApi.Entities;
using MongoDB.Driver;

namespace CategoryApi.Settings
{
    public class MongoContext : IMongoContext
    {
        public MongoContext(IMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Categories = database.GetCollection<Category>(settings.CollectionName);
        }

        public IMongoCollection<Category> Categories { get; set; }
    }
}