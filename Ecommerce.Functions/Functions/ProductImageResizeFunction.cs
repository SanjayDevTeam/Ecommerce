using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Functions.Functions;

public class ProductImageResizeFunction
{
    private readonly ILogger<ProductImageResizeFunction> _logger;

    public ProductImageResizeFunction(
        ILogger<ProductImageResizeFunction> logger)
    {
        _logger = logger;
    }

    [Function("ProductImageResize")]
    public void Run(
        [BlobTrigger(
            "product-images/{name}")]
        Stream stream,
        string name)
    {
        _logger.LogInformation(
            $"Processing image {name}");
    }
}