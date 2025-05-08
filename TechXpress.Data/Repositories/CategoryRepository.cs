using Microsoft.EntityFrameworkCore;
using TechXpress.Data.Models;
using TechXpress.Data;

namespace TechXpress.Data.Repositories;
public interface ICategoryRepository
{
    Task AddCategory(Category category);
    Task UpdateCategory(Category category);
    Task<Category?> GetCategoryById(int id);
    Task DeleteCategory(Category category);
    Task<IEnumerable<Category>> GetCategorys();
}

public class CategoryRepository : ICategoryRepository
{
    

    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCategory(Category category)
    {
        _context.Categorys.Add(category);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCategory(Category category)
    {
        _context.Categorys.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategory(Category category)
    {
        _context.Categorys.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryById(int id)
    {
        return await _context.Categorys.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetCategorys()
    {
        return await _context.Categorys.ToListAsync();
    }


}
