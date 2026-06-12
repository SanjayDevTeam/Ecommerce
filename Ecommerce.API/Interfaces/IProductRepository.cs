using Ecommerce.API.Entities;
using Ecommerce.API.DTOs;

namespace Ecommerce.API.Interfaces;

public interface IProductRepository
{
    
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);

    Task<Product> AddAsync(Product product);

    Task<List<Product>> GetProductsAsync(ProductQueryParams query);

}