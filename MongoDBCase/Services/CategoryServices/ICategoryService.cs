using MongoDBCase.Dtos.CategoryDtos;

namespace MongoDBCase.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoriesAsync();

        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
        Task DeleteCategoryAsync(string id);
        Task<GetCategoryByIdDTO> GetCategoryByIdAsync(string id);
    }
}
