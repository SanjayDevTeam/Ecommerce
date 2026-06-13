using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Functions.Functions;

public class DailySalesReportFunction
{
    private readonly ILogger<DailySalesReportFunction> _logger;

    public DailySalesReportFunction(
        ILogger<DailySalesReportFunction> logger)
    {
        _logger = logger;
    }

    [Function("DailySalesReport")]
    public void Run(
        [TimerTrigger("0 0 0 * * *")]
        TimerInfo timer)
    {
        _logger.LogInformation(
            "Generating daily sales report");
    }
}