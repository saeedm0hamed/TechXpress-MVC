﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechXpress.Web.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var categorys = await _UnitOfWork.Category.GetCategorys();
            return View(categorys);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            try
            {
                var categoryToAdd = new Category { CategoryName = category.CategoryName, Id = category.Id };
                await _UnitOfWork.Category.AddCategory(categoryToAdd);
                TempData["successMessage"] = "Category added successfully";
                return RedirectToAction(nameof(AddCategory));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Category could not added!";
                return View(category);
            }

        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _UnitOfWork.Category.GetCategoryById(id);
            if (category is null)
                throw new InvalidOperationException($"Category with id: {id} does not found");
            var categoryToUpdate = new CategoryViewModel
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
            return View(categoryToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel categoryToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryToUpdate);
            }
            try
            {
                var category = new Category { CategoryName = categoryToUpdate.CategoryName, Id = categoryToUpdate.Id };
                await _UnitOfWork.Category.UpdateCategory(category);
                TempData["successMessage"] = "Category is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Category could not updated!";
                return View(categoryToUpdate);
            }

        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _UnitOfWork.Category.GetCategoryById(id);
            if (category is null)
                throw new InvalidOperationException($"Category with id: {id} does not found");
            await _UnitOfWork.Category.DeleteCategory(category);
            return RedirectToAction(nameof(Index));

        }

    }
}
