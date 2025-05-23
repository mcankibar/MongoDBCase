using Microsoft.AspNetCore.Mvc;

namespace MongoDBCase.ViewComponents
{
    public class _DefaultScriptComponentPartial: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
    
}
