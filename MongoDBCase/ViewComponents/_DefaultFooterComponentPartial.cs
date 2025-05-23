using Microsoft.AspNetCore.Mvc;

namespace MongoDBCase.ViewComponents
{
    public class _DefaultFooterComponentPartial : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
} 
