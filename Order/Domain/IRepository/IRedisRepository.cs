using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepository
{
    public interface IRedisRepository
    {
        Task<Order> Get(string key);
        Task<Order> Set(Order order);
        Task<bool> Delete(string username);
    }
}