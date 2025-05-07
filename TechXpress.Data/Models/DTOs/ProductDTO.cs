﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TechXpress.Data.Models.DTOs;
public class ProductDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? ProductName { get; set; }

    [Required]
    [MaxLength(40)]
    public string? AuthorName { get; set; }
    [Required]
    public double Price { get; set; }
    public string? Image { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IEnumerable<SelectListItem>? CategoryList { get; set; }
}
