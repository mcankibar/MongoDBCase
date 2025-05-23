using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDBCase.Dtos.CategoryDtos;
using MongoDBCase.Dtos.ProductDtos;
using MongoDBCase.Dtos.SliderDtos;
using MongoDBCase.Services.CategoryServices;
using MongoDBCase.Services.ProductServices;
using MongoDBCase.Services.SliderServices;

namespace MongoDBCase.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        private readonly ISliderService _sliderService;

        public AdminController(IProductService productService, ICategoryService categoryService,ISliderService sliderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sliderService = sliderService;
        }

       


        #region Category Operations

        public async Task<IActionResult> GetAllCategories()
        {
            var values = await _categoryService.GetAllCategoriesAsync();
            return View(values);
        }
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(GetAllCategories));
        }

        [HttpGet]

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            
                await _categoryService.CreateCategoryAsync(createCategoryDTO);
                return RedirectToAction(nameof(GetAllCategories));
            
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
            return RedirectToAction(nameof(GetAllCategories));
        }

        #endregion


        #region Product Operations
        public async Task<IActionResult> GetAllProducts()
        {
            var values = await _productService.GetAllProductsAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(GetAllProducts));
        }


        [HttpGet]

        public async Task<IActionResult> UpdateProduct(string id)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();
            var value = await _productService.GetProductByIdAsync(id);
            return View(value);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            await _productService.UpdateProductAsync(updateProductDTO);
            return RedirectToAction(nameof(GetAllProducts));
        }

        [HttpGet]

        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(createProductDTO);
                return RedirectToAction(nameof(GetAllProducts));
            }
            return View(createProductDTO);
        }
        #endregion


        #region Slider Operations

        public async Task<IActionResult> GetAllSliders()
        {
            var values = await _sliderService.GetAllSlidersAsync();
            return View(values);
        }
        public async Task<IActionResult> DeleteSlider(string id)
        {
            await _sliderService.DeleteSliderAsync(id);
            return RedirectToAction(nameof(GetAllSliders));
        }

        [HttpGet]

        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateSlider(CreateSliderDTO createSliderDTO)
        {

            await _sliderService.CreateSliderAsync(createSliderDTO);
            return RedirectToAction(nameof(GetAllSliders));

        }

        [HttpGet]

        public async Task<IActionResult> UpdateSlider(string id)
        {
            var value = await _sliderService.GetSliderByIdAsync(id);
            return View(value);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateSlider(UpdateSliderDTO updateSliderDTO)
        {
            await _sliderService.UpdateSliderAsync(updateSliderDTO);
            return RedirectToAction(nameof(GetAllSliders));
        }

        #endregion
    }
}
