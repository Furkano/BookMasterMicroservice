using System.Threading.Tasks;
using CoreCommand.Entities;
using CoreCommand.IRepository.Base;

namespace CoreCommand.IRepository
{
    public interface IBookRepository : IBaseRepository<Book>
    {
         Task<Book> GetBookById(int id);
         Task<Book> GetBookByCategoryId(string categoryId);
    }
}