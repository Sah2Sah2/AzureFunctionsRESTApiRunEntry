using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RESTApiAzureFunctions.Models;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using RESTApiAzureFunctions.Data;
using Microsoft.EntityFrameworkCore;

namespace RESTApiAzureFunctions
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly RunDbContext _db;  // Add db

        // Inject RunDbContext via constructor
        public Function1(ILogger<Function1> logger, RunDbContext db)
        {
            _logger = logger;
            _db = db;   // Assign DbContext
        }

        [Function("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "runs")] HttpRequest req)
        {
            _logger.LogInformation("HTTP trigger function processed a request.");

            if (req.Method == HttpMethods.Get)
            {
                var runs = await _db.Runs.ToListAsync();
                return new OkObjectResult(runs);
            }
            else if (req.Method == HttpMethods.Post)
            {
                // Read the request body
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                // Deserialize JSON 
                var newRun = JsonSerializer.Deserialize<RunEntry>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (newRun is null)
                    return new BadRequestObjectResult("Invalid JSON");

                // Create a unique ID 
                newRun.Id = Guid.NewGuid().ToString();

                // Save to DB
                _db.Runs.Add(newRun);
                await _db.SaveChangesAsync();

                return new OkObjectResult(newRun);
            }

            return new BadRequestResult();
        }
    }
}
