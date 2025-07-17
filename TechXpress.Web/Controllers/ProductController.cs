using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechXpress.Services;

namespace TechXpress.Web.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class ProductController : Controller
{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IFileService _fileService;

    public ProductController(IUnitOfWork UnitOfWork, IFileService fileService)
    {
        _UnitOfWork = UnitOfWork;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _UnitOfWork.Product.GetProducts();
        return View(products);
    }

    public async Task<IActionResult> AddProduct()
    {
        var categorySelectList = (await _UnitOfWork.Category.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
        });
        ProductViewModel productToAdd = new() { CategoryList = categorySelectList };
        return View(productToAdd);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductViewModel productToAdd)
    {
        var categorySelectList = (await _UnitOfWork.Category.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
        });
        productToAdd.CategoryList = categorySelectList;

        if (!ModelState.IsValid)
            return View(productToAdd);

        try
        {
            if (productToAdd.ImageFile != null)
            {
                if (productToAdd.ImageFile.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(productToAdd.ImageFile, allowedExtensions);
                productToAdd.Image = imageName;
            }
            Product product = new()
            {
                Id = productToAdd.Id,
                ProductName = productToAdd.ProductName,
                Image = productToAdd.Image,
                CategoryId = productToAdd.CategoryId,
                Price = productToAdd.Price,
                Description = productToAdd.Description
            };
            await _UnitOfWork.Product.AddProduct(product);
            TempData["successMessage"] = "Product is added successfully";
            return RedirectToAction(nameof(AddProduct));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(productToAdd);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(productToAdd);
        }
        catch (Exception)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(productToAdd);
        }
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> UpdateProduct(int id)
    {
        var product = await _UnitOfWork.Product.GetProductById(id);
        if (product == null)
        {
            TempData["errorMessage"] = $"Product with the id: {id} does not found";
            return RedirectToAction(nameof(Index));
        }
        var categorySelectList = (await _UnitOfWork.Category.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
            Selected = category.Id == product.CategoryId
        });
        ProductViewModel productToUpdate = new()
        {
            Id = product.Id,
            CategoryList = categorySelectList,
            ProductName = product.ProductName,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Image = product.Image,
            Description = product.Description
        };
        return View(productToUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productToUpdate)
    {
        var categorySelectList = (await _UnitOfWork.Category.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
            Selected = category.Id == productToUpdate.CategoryId
        });
        productToUpdate.CategoryList = categorySelectList;

        if (!ModelState.IsValid)
        {
            return View(productToUpdate);
        }           

        try
        {
            string oldImage = "";
            if (productToUpdate.ImageFile != null)
            {
                if (productToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(productToUpdate.ImageFile, allowedExtensions);
                oldImage = productToUpdate.Image;
                productToUpdate.Image = imageName;
            }
            Product product = new()
            {
                Id = productToUpdate.Id,
                ProductName = productToUpdate.ProductName,
                CategoryId = productToUpdate.CategoryId,
                Price = productToUpdate.Price,
                Image = productToUpdate.Image,
                Description = productToUpdate.Description
            };
            await _UnitOfWork.Product.UpdateProduct(product);
            if (!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "Product is updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(productToUpdate);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(productToUpdate);
        }
        catch (Exception)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(productToUpdate);
        }

        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var product = await _UnitOfWork.Product.GetProductById(id);
            if (product == null)
            {
                TempData["errorMessage"] = $"Product with the id: {id} does not found";
            }
            else
            {
                await _UnitOfWork.Product.DeleteProduct(product);
                if (!string.IsNullOrWhiteSpace(product.Image))
                {
                    _fileService.DeleteFile(product.Image);
                }
                TempData["successMessage"] = "Product deleted successfully";
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (Exception)
        {
            TempData["errorMessage"] = "Error on deleting the data";
        }
        return RedirectToAction(nameof(Index));
    }
}