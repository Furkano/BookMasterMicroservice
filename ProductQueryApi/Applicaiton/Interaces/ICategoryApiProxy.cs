using System.Threading.Tasks;
using Applicaiton.Responces;

namespace Applicaiton.Interaces
{
    public interface ICategoryApiProxy
    {
         Task<CategoryResponse> GetCategoryById(string id);
    }
}