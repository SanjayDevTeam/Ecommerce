using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly EcommerceDbContext _context;

    public ProductController(EcommerceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Products.ToList());
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        _context.Products.Add(product);

        _context.SaveChanges();

        return Ok(product);
    }
}