using TechXpress.Data.Models;
using TechXpress.Data.Models.ViewModels;

namespace TechXpress.Services.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> Orders(bool getAll = false);
    Task ChangeOrderStatus(UpdateOrderStatusModel data);
    Task TogglePaymentStatus(int orderId);
    Task<Order?> GetOrderById(int id);
    Task<IEnumerable<OrderStatus>> GetOrderStatuses();

}