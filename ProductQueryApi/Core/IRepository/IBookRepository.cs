using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Nest;

namespace Core.IRepository
{
    public interface IBookRepository : IBaseRepository
    {
        Task<IEnumerable<Book>> SuggestSearchAsync(string suggestText, int maxItemCount = 100);
        Task<IEnumerable<Book>> GetSearchAsync(string searchText,   int skipItemCount = 0, int maxItemCount = 100);
    }
}