using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Functions.Functions;

public class PaymentProcessingFunction
{
    private readonly ILogger<PaymentProcessingFunction> _logger;

    public PaymentProcessingFunction(
        ILogger<PaymentProcessingFunction> logger)
    {
        _logger = logger;
    }

    [Function("PaymentProcessing")]
    public void Run(
        [TimerTrigger("0 */1 * * * *")]
        TimerInfo timer)
    {
        _logger.LogInformation(
            "Processing pending payments");
    }
}