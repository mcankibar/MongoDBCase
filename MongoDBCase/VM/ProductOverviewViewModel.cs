using MongoDBCase.Dtos.CategoryDtos;
using MongoDBCase.Dtos.ProductDtos;
using MongoDBCase.Entities;

namespace MongoDBCase.VM
{
    public class ProductOverviewViewModel
    {
        public List<ResultProductDTO> Products { get; set; }
        public List<ResultCategoryDTO> Categories { get; set; }
    }
}
