using Microsoft.AspNetCore.Mvc;
using MongoDBCase.Services.CategoryServices;
using MongoDBCase.Services.ProductServices;
using MongoDBCase.VM;

namespace MongoDBCase.ViewComponents
{
    public class _DefaultProductOverviewComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public _DefaultProductOverviewComponentPartial(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = new ProductOverviewViewModel
            {
                Products = await _productService.GetAllProductsAsync(),
                Categories = await _categoryService.GetAllCategoriesAsync()
            };


            return View(model);
        }
    }
   
}
