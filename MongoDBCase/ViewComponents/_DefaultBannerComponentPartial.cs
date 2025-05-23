using Microsoft.AspNetCore.Mvc;
using MongoDBCase.Services.CategoryServices;

namespace MongoDBCase.ViewComponents
{
    public class _DefaultBannerComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _DefaultBannerComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoriesAsync();
            return View(values);
        }
    }
   
}
