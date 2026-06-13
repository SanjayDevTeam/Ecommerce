using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Functions.Functions;

public class AbandonedCartCleanupFunction
{
    private readonly ILogger<AbandonedCartCleanupFunction> _logger;

    public AbandonedCartCleanupFunction(
        ILogger<AbandonedCartCleanupFunction> logger)
    {
        _logger = logger;
    }

    [Function("AbandonedCartCleanup")]
    public void Run(
        [TimerTrigger("0 0 * * * *")]
        TimerInfo timer)
    {
        _logger.LogInformation(
            "Removing abandoned carts");
    }
}