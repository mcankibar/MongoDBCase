using Microsoft.AspNetCore.Mvc;

namespace MongoDBCase.ViewComponents
{
    public class _DefaultHeadComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
   
}
