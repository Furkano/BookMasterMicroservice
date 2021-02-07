using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IBasketRepository<T>
    {
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<Boolean> Delete(T entity);
    }
}