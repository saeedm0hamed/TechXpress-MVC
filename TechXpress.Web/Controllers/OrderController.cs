using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechXpress.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public OrderController(IUnitOfWork OrderRepo)
        {
            _UnitOfWork = OrderRepo;
        }
        public async Task<IActionResult> Orders()
        {
            var orders = await _UnitOfWork.Order.Orders();
            return View(orders);
        }
    }
}
