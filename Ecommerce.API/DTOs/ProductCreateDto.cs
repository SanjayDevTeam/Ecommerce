namespace Ecommerce.API.DTOs;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string Description { get; set; } = string.Empty;
}