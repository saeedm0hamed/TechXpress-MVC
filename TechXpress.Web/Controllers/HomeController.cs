using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechXpress.Data.Models.ViewModels;
using TechXpress.Services.Repositories;

namespace TechXpress.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm = "", int categoryId = 0, string sortBy = "", double? minPrice = null, double? maxPrice = null)  
        {
            // Get all products first
            IEnumerable<Product> products = await _UnitOfWork.Home.GetProducts(sterm, categoryId);

            // Apply price filtering if specified
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            // Apply sorting
            products = sortBy switch
            {
                "name_asc" => products.OrderBy(p => p.ProductName),
                "name_desc" => products.OrderByDescending(p => p.ProductName),
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                _ => products.OrderBy(p => p.Id) // Default sorting
            };

            // Get categories
            IEnumerable<Category> categorys = await _UnitOfWork.Home.Categorys();

            // Create view model
            ProductDisplayModel productModel = new ProductDisplayModel
            {
                Products = products.ToList(),
                Categorys = categorys,
                STerm = sterm,
                CategoryId = categoryId,
                SortBy = sortBy,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            return View(productModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}