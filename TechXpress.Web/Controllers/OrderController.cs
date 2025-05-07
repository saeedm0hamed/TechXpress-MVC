using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechXpress.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _OrderRepo;

        public OrderController(IOrderRepository OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<IActionResult> Orders()
        {
            var orders = await _OrderRepo.Orders();
            return View(orders);
        }
    }
}
