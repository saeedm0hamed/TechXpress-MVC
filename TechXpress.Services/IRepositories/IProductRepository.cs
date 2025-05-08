using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Services.Repositories
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task DeleteProduct(Product product);
        Task<Product?> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
        Task UpdateProduct(Product product);
    }
}
