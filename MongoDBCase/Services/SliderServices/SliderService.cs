using AutoMapper;
using MongoDB.Driver;
using MongoDBCase.Dtos.CategoryDtos;
using MongoDBCase.Dtos.SliderDtos;
using MongoDBCase.Entities;
using MongoDBCase.Settings;

namespace MongoDBCase.Services.SliderServices
{
    public class SliderService :ISliderService
    {
        private readonly IMapper _mapper;

        private readonly IMongoCollection<Slider> _sliderCollection;

        public SliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _sliderCollection = database.GetCollection<Slider>(_databaseSettings.SliderCollectionName);
            _mapper = mapper;
        }



        public async Task CreateSliderAsync(CreateSliderDTO createSliderDTO)
        {
            var slider = _mapper.Map<Slider>(createSliderDTO);
            await _sliderCollection.InsertOneAsync(slider);

        }

        public async Task DeleteSliderAsync(string id)
        {

            await _sliderCollection.DeleteOneAsync(x => x.SliderId == id);
        }

        public async Task<List<ResultSliderDTO>> GetAllSlidersAsync()
        {
            var sliders = await _sliderCollection.Find(_ => true).ToListAsync();

            return _mapper.Map<List<ResultSliderDTO>>(sliders);

        }

      

        public async Task UpdateSliderAsync(UpdateSliderDTO updateSliderDTO)
        {
            var slider = _mapper.Map<Slider>(updateSliderDTO);
            await _sliderCollection.FindOneAndReplaceAsync(x => x.SliderId == updateSliderDTO.SliderId, slider);
        }

        public async Task<GetSliderByIdDTO> GetSliderByIdAsync(string id)
        {
            var value = await _sliderCollection.Find(x => x.SliderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetSliderByIdDTO>(value);
        }
    }
}
