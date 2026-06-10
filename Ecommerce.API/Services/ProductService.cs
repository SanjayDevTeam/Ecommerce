using Ecommerce.API.DTOs;
using Ecommerce.API.Entities;
using Ecommerce.API.Interfaces;

namespace Ecommerce.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();

        return products.Select(x => new ProductResponseDto
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price
        }).ToList();
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            Description = dto.Description
        };

        await _repository.AddAsync(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
}