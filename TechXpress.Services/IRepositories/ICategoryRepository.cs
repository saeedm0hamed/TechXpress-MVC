using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Services.IRepositories
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task<Category?> GetCategoryById(int id);
        Task DeleteCategory(Category category);
        Task<IEnumerable<Category>> GetCategorys();
    }
}
