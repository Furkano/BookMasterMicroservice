using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Infrastructure.DataContext
{
    // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            
        }
        public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
    }
}