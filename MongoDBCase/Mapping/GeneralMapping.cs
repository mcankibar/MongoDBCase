using AutoMapper;
using MongoDBCase.Dtos.CategoryDtos;
using MongoDBCase.Dtos.ProductDtos;
using MongoDBCase.Dtos.SliderDtos;
using MongoDBCase.Entities;

namespace MongoDBCase.Mapping
{
    public class GeneralMapping :Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, ResultCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDTO>().ReverseMap();

            CreateMap<CreateProductDTO, Product>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(url => new ProductImage { ProductImageUrl = url }).ToList()));

            CreateMap<Product, CreateProductDTO>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(img => img.ProductImageUrl).ToList()));


            CreateMap<Product, ResultProductDTO>()
                 .ForMember(dest => dest.ProductImages,
                     opt => opt.MapFrom(src => src.ProductImages
                         .Select(img => img.ProductImageUrl).ToList()));

            CreateMap<ResultProductDTO, Product>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(url => new ProductImage { ProductImageUrl = url }).ToList()));

            CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(url => new ProductImage { ProductImageUrl = url }).ToList()));

            CreateMap<Product, UpdateProductDTO>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(img => img.ProductImageUrl).ToList()));

            CreateMap<Product, GetProductByIdDTO>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(img => img.ProductImageUrl).ToList()));

            CreateMap<GetProductByIdDTO, Product>()
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(url => new ProductImage { ProductImageUrl = url }).ToList()));


            CreateMap<Slider, CreateSliderDTO>().ReverseMap();
            CreateMap<Slider, ResultSliderDTO>().ReverseMap();
            CreateMap<Slider, UpdateSliderDTO>().ReverseMap();
            CreateMap<Slider, GetSliderByIdDTO>().ReverseMap();
        }




    }
}
