using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Functions.Functions;

public class InventoryUpdateFunction
{
    private readonly ILogger<InventoryUpdateFunction> _logger;

    public InventoryUpdateFunction(
        ILogger<InventoryUpdateFunction> logger)
    {
        _logger = logger;
    }

    [Function("InventoryUpdate")]
    public void Run(
        [TimerTrigger("0 */5 * * * *")]
        TimerInfo timer)
    {
        _logger.LogInformation(
            "Inventory synchronization started");
    }
}