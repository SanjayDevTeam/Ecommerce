using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Ecommerce.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ecommerce.API.DTOs;

namespace Ecommerce.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly EcommerceDbContext _context;

    public ProductRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<List<Product>> GetProductsAsync(ProductQueryParams query)
    {

    IQueryable<Product> products = _context.Products.AsNoTracking();

    if (!string.IsNullOrWhiteSpace(query.Search))
    {
        products = products.Where(x =>
            x.Name.Contains(query.Search));
    }

    if (query.SortBy?.ToLower() == "price")
    {
        products = products.OrderBy(x => x.Price);
    }

    return await products
        .Skip((query.Page - 1) * query.PageSize)
        .Take(query.PageSize)
        .ToListAsync();
    }

}