using Microsoft.EntityFrameworkCore;
using TechXpress.Data.Models;
using TechXpress.Data;

namespace TechXpress.Data.Repositories
    
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetProducts(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<Category>> Categorys();
    }
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> Categorys()
        {
            return await _db.Categorys.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts(string sTerm = "", int categoryId = 0)
        {
            var productQuery = _db.Products
               .AsNoTracking()
               .Include(x => x.Category)
               .Include(x => x.Stock)
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sTerm))
            {
                productQuery = productQuery.Where(b => b.ProductName.StartsWith(sTerm.ToLower()));
            }

            if (categoryId > 0)
            {
                productQuery = productQuery.Where(b => b.CategoryId == categoryId);
            }

            var products = await productQuery
                .AsNoTracking()
                .Select(product => new Product
                {
                    Id = product.Id,
                    Image = product.Image,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    CategoryName = product.Category.CategoryName,
                    Description = product.Description,
                    Quantity = product.Stock == null ? 0 : product.Stock.Quantity
                }).ToListAsync();
            return products;

        }
    }
}