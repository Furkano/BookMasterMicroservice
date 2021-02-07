using CategoryApi.Entities;
using MongoDB.Driver;

namespace CategoryApi.Settings
{
    public interface IMongoContext
    {
        IMongoCollection<Category> Categories { get; set; }
    }
}