﻿namespace TechXpress.Data.Models.ViewModels
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
    }
}
