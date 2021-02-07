namespace CategoryApi.Settings
{
    public class MongoSettings:IMongoSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}