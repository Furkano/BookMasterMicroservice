
using StackExchange.Redis;

namespace Domain.IRepository
{
    public class IRedisContext
    {
        public IDatabase Redis { get; }
    }
}