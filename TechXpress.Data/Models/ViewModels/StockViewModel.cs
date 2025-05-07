using System.ComponentModel.DataAnnotations;

namespace TechXpress.Data.Models.ViewModels
{
    public class StockViewModel
    {
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }
    }
}
