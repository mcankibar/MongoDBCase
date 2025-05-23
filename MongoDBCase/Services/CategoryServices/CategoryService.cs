using AutoMapper;
using MongoDB.Driver;
using MongoDBCase.Dtos.CategoryDtos;
using MongoDBCase.Entities;
using MongoDBCase.Settings;

namespace MongoDBCase.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;

        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }



        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var category = _mapper.Map<Category>(createCategoryDTO);
            await _categoryCollection.InsertOneAsync(category);

        }

        public async Task DeleteCategoryAsync(string id)
        {

            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryCollection.Find(_ => true).ToListAsync();

            return _mapper.Map<List<ResultCategoryDTO>>(categories);

        }

        public async Task<GetCategoryByIdDTO> GetCategoryByIdAsync(string id)
        {
            var value = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetCategoryByIdDTO>(value);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategorytDTO)
        {
            var category = _mapper.Map<Category>(updateCategorytDTO);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategorytDTO.CategoryId, category);
        }
    }
}
