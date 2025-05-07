﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TechXpress.Data.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Product? Product { get; set; }
    }
}
