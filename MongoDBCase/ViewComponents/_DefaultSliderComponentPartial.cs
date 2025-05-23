using Microsoft.AspNetCore.Mvc;
using MongoDBCase.Dtos.SliderDtos;
using MongoDBCase.Services.SliderServices;

namespace MongoDBCase.ViewComponents
{
    public class _DefaultSliderComponentPartial : ViewComponent
    {

        private readonly ISliderService _sliderService;

        public _DefaultSliderComponentPartial(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ResultSliderDTO> values = await _sliderService.GetAllSlidersAsync();
            return View(values);
        }
    }
}
