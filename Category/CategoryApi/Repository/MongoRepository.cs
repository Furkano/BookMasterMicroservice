using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CategoryApi.Entities;
using CategoryApi.Repository.IRepository;
using CategoryApi.Settings;
using MongoDB.Driver;

namespace CategoryApi.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        // private readonly MongoContext _mongoContext;
        public IMongoCollection<Category> Categories { get; set; }

        public MongoRepository(IMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Categories = database.GetCollection<Category>(settings.CollectionName);
        }
        
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await Categories
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await Categories
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryByName(string name)
        {
            // FilterDefinition<Category> filter = Builders<Category>.Filter.ElemMatch(p => p.Name, name);
            return await Categories
                .Find(p=>p.Name==name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryByParent(string parent)
        {
            // FilterDefinition<Category> filter = Builders<Category>.Filter.ElemMatch(p => p.Parent, parent);
            var result = await Categories.Find(p=>p.Parent==parent).ToListAsync();
            return result;
        }
        public async Task<bool> CreateMany(IEnumerable<Category> models)
        {
            await Categories.InsertManyAsync(models);
            return true;
        }

        public async Task<bool> Create(Category model)
        {
            await Categories.InsertOneAsync(model);
            return true;
        }

        public async Task<bool> Update(Category model)
        {
            var updateResult =
                await Categories
                    .ReplaceOneAsync(filter: f => f.Id == model.Id, replacement: model);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await Categories
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }
    }
}