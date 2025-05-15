using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RESTApiAzureFunctions.Models;
using System.Text.Json;

namespace RESTApiAzureFunctions
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "runs")] HttpRequest Req)
        {
            _logger.LogInformation("HTTP trigger function processed a request.");

            if (Req.Method == HttpMethods.Get)
            {
                return new ObjectResult(RunDataStore.Runs);
            }
            else if (Req.Method == HttpMethods.Post)
            {
                // Read the req body
                string requestBody = await new StreamReader(Req.Body).ReadToEndAsync();

                // Desrialize JSON 
                var newRun = JsonSerializer.Deserialize<RunEntry>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (newRun is null)
                    return new BadRequestObjectResult("Invalid JSON");

                // Create a unique ID 
                newRun.Id = Guid.NewGuid().ToString();

                // Add to the momentary list
                RunDataStore.Runs.Add(newRun);

                return new OkObjectResult(newRun);
            }
            return new BadRequestResult();
        }
    }
}
