﻿namespace TechXpress.Data.Models.DTOs;

public class OrderDetailModalDTO
{
    public string DivId { get; set; }
    public IEnumerable<OrderDetail> OrderDetail { get; set; }
}
