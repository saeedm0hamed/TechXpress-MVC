using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TechXpress.Data.Repositories
{
    public interface IUnitOfWork
    {
        ICartRepository Cart { get; }
        ICategoryRepository Category { get; }
        IHomeRepository Home { get; }
        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        IStockRepository Stock { get; }
        Task<int> Save();

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public ICartRepository Cart { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IHomeRepository Home { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IProductRepository Product { get; private set; }
        public IStockRepository Stock { get; private set; }


        public UnitOfWork(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)

        {
            _db = db;
            Cart = new CartRepository(_db, httpContextAccessor, userManager);
            Category = new CategoryRepository(_db);
            Home = new HomeRepository(_db);
            Order = new OrderRepository(_db, userManager, httpContextAccessor);
            Product = new ProductRepository(_db);
            Stock = new StockRepository(_db);
        }

        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
