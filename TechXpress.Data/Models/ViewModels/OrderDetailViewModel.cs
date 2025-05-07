namespace TechXpress.Data.Models.ViewModels;

public class OrderDetailViewModel
{
    public string DivId { get; set; }
    public IEnumerable<OrderDetail> OrderDetail { get; set; }
}
