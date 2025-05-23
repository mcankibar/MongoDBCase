using MongoDBCase.Dtos.CategoryDtos;
using MongoDBCase.Dtos.SliderDtos;

namespace MongoDBCase.Services.SliderServices
{
    public interface ISliderService
    {
        Task<List<ResultSliderDTO>> GetAllSlidersAsync();

        Task CreateSliderAsync(CreateSliderDTO createSliderDTO);
        Task UpdateSliderAsync(UpdateSliderDTO updateSliderDTO);
        Task DeleteSliderAsync(string id);

        Task<GetSliderByIdDTO> GetSliderByIdAsync(string id);
    }
}
