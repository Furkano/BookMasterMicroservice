using Domain.IRepository;
using StackExchange.Redis;

namespace Infrastructure.DataContext
{
    public class RedisContext : IRedisContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public RedisContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            // var conn = ConnectionMultiplexer.Connect("localhost:6379");
            // Redis = conn.GetDatabase();
            Redis = redisConnection.GetDatabase();
        }
        
        public new IDatabase Redis { get; }
    }
}