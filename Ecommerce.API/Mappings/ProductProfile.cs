using AutoMapper;
using Ecommerce.API.DTOs;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDto, Product>();

        CreateMap<Product, ProductResponseDto>();
    }
}