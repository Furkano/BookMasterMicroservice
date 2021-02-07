
#nullable enable
using MongoDB.Bson.Serialization.Attributes;

namespace CategoryApi.Entities
{
    public class Category : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        public string? Parent { get; set; }
    }
}