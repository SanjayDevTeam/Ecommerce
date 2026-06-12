using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.API.DTOs;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.RateLimiting;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("fixed")]
public class ProductController : ControllerBase
{
    private readonly EcommerceDbContext _context;
    private readonly IProductService _productService;

    public ProductController(EcommerceDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }


    // [HttpGet]
    // public IActionResult Get()
    // {
    //     return Ok(_context.Products.ToList());
    // }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        _context.Products.Add(product);

        _context.SaveChanges();

        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParams query)
    {
        return Ok(await _productService.GetProductsAsync(query));
        }

}