using Ecommerce.API.DTOs;
using Ecommerce.API.Entities;
using Ecommerce.API.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Ecommerce.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository,IMapper mapper, IDistributedCache cache, ILogger<ProductService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
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

    var cachedData =
        await _cache.GetStringAsync(key);

    if (!string.IsNullOrEmpty(cachedData))
    {
        _logger.LogInformation(
            "CACHE HIT: {CacheKey}",
            key);

        return JsonSerializer.Deserialize<
            List<ProductResponseDto>>(cachedData)!;
    }

    _logger.LogInformation(
        "CACHE MISS: {CacheKey}",
        key);

    var products =
        await _repository.GetProductsAsync(query);

    var result =
        _mapper.Map<List<ProductResponseDto>>(products);

    var json =
        JsonSerializer.Serialize(result);

    await _cache.SetStringAsync(
        key,
        json,
        new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow =
                TimeSpan.FromMinutes(10)
        });

    _logger.LogInformation(
        "CACHE STORED: {CacheKey}",
        key);

    return result;
}
}