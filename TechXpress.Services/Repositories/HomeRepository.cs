using Microsoft.EntityFrameworkCore;
using TechXpress.Data.Models;
using TechXpress.Data;

namespace TechXpress.Services.Repositories
{
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
            // this code is demonstrated in the video tutorial.
            // It contains very bad practice.
            // 1st bad practice is in the where condtion
            // 2nd one is: filter by category. I am loading all the data into memory then filtering it. Performance will decrease with data size.
            // I have commented out that code and rewrite it. 

            //sTerm = sTerm.ToLower();
            //IEnumerable<Product> products = await (from product in _db.Products
            //             join category in _db.Categorys
            //             on product.CategoryId equals category.Id
            //             join stock in _db.Stocks
            //             on product.Id equals stock.ProductId
            //             into product_stocks
            //             from productWithStock in product_stocks.DefaultIfEmpty()
            //             where string.IsNullOrWhiteSpace(sTerm) || (product != null && product.ProductName.ToLower().StartsWith(sTerm))
            //             select new Product
            //             {
            //                 Id = product.Id,
            //                 Image = product.Image,
            //                 AuthorName = product.AuthorName,
            //                 ProductName = product.ProductName,
            //                 CategoryId = product.CategoryId,
            //                 Price = product.Price,
            //                 CategoryName = category.CategoryName,
            //                 Quantity=productWithStock==null? 0:productWithStock.Quantity
            //             }
            //             ).ToListAsync();


            //if (categoryId > 0)
            //{

            //    products = products.Where(a => a.CategoryId == categoryId).ToList();
            //}

            // refactored code
            // In this code we are first building query, then rebuilding that query on the basis of filter. Query is translated into sql when we call .ToListAsync() method.

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
                    AuthorName = product.AuthorName,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    CategoryName = product.Category.CategoryName,
                    Quantity = product.Stock == null ? 0 : product.Stock.Quantity
                }).ToListAsync();
            return products;

        }
    }
}
