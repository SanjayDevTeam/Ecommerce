using Ecommerce.API.DTOs;
using Ecommerce.API.Entities;
using Ecommerce.API.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace Ecommerce.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductService(IProductRepository repository,IMapper mapper, IMemoryCache cache)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();

        return _mapper.Map<List<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        await _repository.AddAsync(product);

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<List<ProductResponseDto>>
GetProductsAsync(ProductQueryParams query)
{
    string key =
$"products-{query.Page}-{query.PageSize}";

if (_cache.TryGetValue(key,
out List<ProductResponseDto>? cachedProducts))
{
    return cachedProducts!;
}

    var products = await _repository.GetProductsAsync(query);
    var result =
_mapper.Map<List<ProductResponseDto>>(products);

  _cache.Set(
key,
result,
TimeSpan.FromMinutes(5));

return result;
}

}