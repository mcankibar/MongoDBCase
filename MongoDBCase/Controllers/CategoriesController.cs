using Microsoft.AspNetCore.Mvc;
using MongoDBCase.Dtos.CategoryDtos;
using MongoDBCase.Services.CategoryServices;

namespace MongoDBCase.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.GetAllCategoriesAsync();
            return View(values);
        }

        [HttpGet]

        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(createCategoryDTO);
                return RedirectToAction(nameof(CategoryList));
            }
            return View(createCategoryDTO);
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var value = await _categoryService.GetCategoryByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDTO);
            return RedirectToAction(nameof(CategoryList));
        }
    }
}
