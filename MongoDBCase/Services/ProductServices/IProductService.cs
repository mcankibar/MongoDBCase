using MongoDBCase.Dtos.ProductDtos;

namespace MongoDBCase.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDTO>> GetAllProductsAsync();

        Task CreateProductAsync(CreateProductDTO createProductDTO);
        Task UpdateProductAsync(UpdateProductDTO updateProductDTO);
        Task DeleteProductAsync(string id);
        Task<GetProductByIdDTO> GetProductByIdAsync(string id);
    }
}
