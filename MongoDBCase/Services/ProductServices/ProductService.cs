using AutoMapper;
using MongoDB.Driver;
using MongoDBCase.Dtos.ProductDtos;
using MongoDBCase.Entities;
using MongoDBCase.Settings;

namespace MongoDBCase.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;

        private readonly IMongoCollection<Product> _productCollection;

        private readonly IMongoCollection<ProductImage> _productImageCollection;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _productImageCollection = database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }
        public async Task CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var productImages = new List<ProductImage>();

            foreach (var imageUrl in createProductDTO.ProductImages)
            {
                var productImage = new ProductImage
                {
                    ProductImageUrl = imageUrl
                };

                await _productImageCollection.InsertOneAsync(productImage);
                productImages.Add(productImage);
            }

            var product = _mapper.Map<Product>(createProductDTO);
            product.ProductImages = productImages;
            await _productCollection.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            // 1. Ürünü bul
            var product = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product == null)
                return;

            // 2. Ürüne ait görselleri sil
            if (product.ProductImages != null && product.ProductImages.Any())
            {
                var imageIds = product.ProductImages
                    .Where(img => img != null && !string.IsNullOrEmpty(img.ProductImageId))
                    .Select(img => img.ProductImageId)
                    .ToList();

                if (imageIds.Any())
                {
                    var filter = Builders<ProductImage>.Filter.In(x => x.ProductImageId, imageIds);
                    await _productImageCollection.DeleteManyAsync(filter);
                }
            }

            // 3. Ürünü sil
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        public async Task<List<ResultProductDTO>> GetAllProductsAsync()
        {
            var values = await _productCollection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<ResultProductDTO>>(values);
        }

        public async Task<GetProductByIdDTO> GetProductByIdAsync(string id)
        {
            var value = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetProductByIdDTO>(value);
        }

        public async Task UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            var existingProduct = await _productCollection
                .Find(x => x.ProductId == updateProductDTO.ProductId)
                .FirstOrDefaultAsync();

            updateProductDTO.ProductImages ??= new List<string>();
            existingProduct.ProductImages ??= new List<ProductImage>();

            // Eğer gelen liste boşsa, tüm görselleri sil
            List<ProductImage> removedImages;
            if (updateProductDTO.ProductImages.Count == 0)
            {
                removedImages = existingProduct.ProductImages.ToList();
            }
            else
            {
                removedImages = existingProduct.ProductImages
                    .Where(img => img != null && !string.IsNullOrEmpty(img.ProductImageUrl) && !updateProductDTO.ProductImages.Contains(img.ProductImageUrl))
                    .ToList();
            }

            foreach (var img in removedImages)
            {
                await _productImageCollection.DeleteOneAsync(x => x.ProductImageId == img.ProductImageId);
                existingProduct.ProductImages.Remove(img);
            }

            // Yeni eklenen görselleri ekle
            foreach (var imageUrl in updateProductDTO.ProductImages)
            {
                if (existingProduct.ProductImages.Any(x => x.ProductImageUrl == imageUrl))
                    continue;

                var productImage = new ProductImage
                {
                    ProductImageUrl = imageUrl
                };
                await _productImageCollection.InsertOneAsync(productImage);
                existingProduct.ProductImages.Add(productImage);
            }

            var updatedProduct = _mapper.Map<Product>(updateProductDTO);
            updatedProduct.ProductImages = existingProduct.ProductImages;

            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDTO.ProductId, updatedProduct);
        }



    }
}
