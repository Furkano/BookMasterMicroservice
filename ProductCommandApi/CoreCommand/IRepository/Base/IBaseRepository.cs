
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCommand.Entities;

namespace CoreCommand.IRepository.Base
{
    public interface IBaseRepository<T> where T :BaseEntity
    {
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Delete(int id);
    }
}