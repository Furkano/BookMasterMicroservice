using System;
using System.Threading.Tasks;
using Domain.IRepository;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using Order = Domain.Entities.Order;

namespace Infrastructure.Repository
{
    public class RedisRepository : IRedisRepository
    {
        // private readonly IRedisContext _context;
        private IDatabase Redis { get; }

        public RedisRepository(ConnectionMultiplexer redisConnection)
        {
            // _context = context ?? throw new ArgumentNullException(nameof(context));
            Redis = redisConnection.GetDatabase();
        }
        public async Task<Order> Get(string key)
        {
            var order = await Redis.StringGetAsync(key);
            return order.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<Order>(order);
        }

        public async Task<Order> Set(Order order)
        {
            var updated = await 
                Redis
                .StringSetAsync(order.Id.ToString(), JsonConvert.SerializeObject(order));
            if (!updated)
            {                
                return null;
            }            
            return await Get(order.Username); 
        }

        public async Task<bool> Delete(string username)
        {
            return await 
                Redis
                .KeyDeleteAsync(username);
        }
    }
}