using System.Linq;
using System.Threading.Tasks;
using CoreCommand.IRepository.Base;
using CoreCommand.Entities;
using CoreCommand.IRepository;
using InfrastructureCommand.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommand.Repository
{
    public class BookRepository : BaseRepository<Book>,IBookRepository
    {
        public BookRepository(BookContext context):base(context)
        {
            
        }
        
        public async Task<Book> GetBookById(int id)
        {
            return await _context.Set<Book>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book> GetBookByCategoryId(string categoryId)
        {
            return await _context.Books.Where(p => p.CategoryId == categoryId).FirstOrDefaultAsync();
        }
    }
}