using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDBCase.Dtos.ProductDtos;
using MongoDBCase.Services.CategoryServices;
using MongoDBCase.Services.ProductServices;

namespace MongoDBCase.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;

            _categoryService = categoryService;

        }
        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductsAsync();
            return View(values);
        }

        [HttpGet]

        public async Task<IActionResult> CreateProduct()
        {

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.v = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {

            await _productService.CreateProductAsync(createProductDTO);
            return RedirectToAction(nameof(ProductList));

        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(ProductList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.v = categories.Select(c => new SelectListItem
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
            return RedirectToAction(nameof(ProductList));
        }
    }
}
