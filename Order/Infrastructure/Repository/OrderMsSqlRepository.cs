﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepository;
using Infrastructure.DataContext;

namespace Infrastructure.Repository
{
    public class OrderMsSqlRepository<T> : IOrderRepository<T> where T : BaseEntity
    {
        private OrderContext AppDbContext { get; set; }

        public OrderMsSqlRepository(OrderContext appDbContext)
        {
            AppDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }
        
        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return AppDbContext.Set<T>().Where(expression);
        }

        public async Task<T> Create(T entity)
        {
            var result = await AppDbContext.Set<T>()
                .AddAsync(entity);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<T> Update(T entity)
        {
            var result= AppDbContext.Set<T>().Update(entity);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Boolean> Delete(T entity)
        {
            AppDbContext.Set<T>().Remove(entity);
            return (await AppDbContext.SaveChangesAsync()) > 0;
        }
    }
}