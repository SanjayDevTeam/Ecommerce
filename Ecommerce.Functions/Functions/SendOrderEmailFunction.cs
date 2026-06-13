using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Ecommerce.Functions.Functions;

public class SendOrderEmailFunction
{
    private readonly ILogger<SendOrderEmailFunction> _logger;

    public SendOrderEmailFunction(
        ILogger<SendOrderEmailFunction> logger)
    {
        _logger = logger;
    }

    [Function("SendOrderEmail")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "post")]
        HttpRequestData req)
    {
        _logger.LogInformation(
            "Sending order email");

        var response =
            req.CreateResponse(HttpStatusCode.OK);

        await response.WriteStringAsync(
            "Email sent successfully");

        return response;
    }
}