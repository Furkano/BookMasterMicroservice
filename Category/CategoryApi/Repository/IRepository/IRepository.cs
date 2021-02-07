using System.Collections.Generic;
using System.Threading.Tasks;
using CategoryApi.Entities;

namespace CategoryApi.Repository.IRepository
{
    public interface IRepository<T> where T :BaseEntity
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(string id);
        Task<IEnumerable<Category>> GetCategoryByName(string name);
        Task<IEnumerable<Category>> GetCategoryByParent(string parent);

        Task<bool> CreateMany(IEnumerable<Category> models);
        Task<bool> Create(Category model);
        Task<bool> Update(Category model);
        Task<bool> Delete(string id);
    }
}