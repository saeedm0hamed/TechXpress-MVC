using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace TechXpress.Data.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
