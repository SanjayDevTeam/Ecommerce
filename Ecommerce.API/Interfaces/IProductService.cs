using Ecommerce.API.DTOs;

namespace Ecommerce.API.Interfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetAllAsync();

    Task<ProductResponseDto> CreateAsync(ProductCreateDto dto);
    
    Task<List<ProductResponseDto>> GetProductsAsync(ProductQueryParams query);
}