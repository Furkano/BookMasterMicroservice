using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreCommand.Entities;
using CoreCommand.IRepository.Base;
using InfrastructureCommand.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommand.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly BookContext _context;

        protected BaseRepository(BookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public async Task<T> Create(T entity)
        {
            await _context.Set<T>()
                .AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Books.Where(p => p.Id == id).FirstOrDefaultAsync();
            _context.Set<Book>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}