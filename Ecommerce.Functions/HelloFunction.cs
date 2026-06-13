using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Ecommerce.Functions;

public class HelloFunction
{
    private readonly ILogger<HelloFunction> _logger;

    public HelloFunction(
        ILogger<HelloFunction> logger)
    {
        _logger = logger;
    }

    [Function("HelloFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Anonymous,
            "get")]
        HttpRequestData req)
    {
        _logger.LogInformation(
            "Hello Function invoked");

        var response =
            req.CreateResponse(HttpStatusCode.OK);

        await response.WriteStringAsync(
            "Hello from Azure Functions");

        return response;
    }
}