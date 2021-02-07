using Microsoft.EntityFrameworkCore;
using CoreCommand.Entities;

namespace InfrastructureCommand.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options):base(options)
        {
            
        }
        public DbSet<Book> Books {get;set;}
        
    }
}