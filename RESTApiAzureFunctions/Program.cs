using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RESTApiAzureFunctions.Data;

var builder = FunctionsApplication.CreateBuilder(args);

// Add configuration from appsettings.json 
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.ConfigureFunctionsWebApplication();

var connectionString = builder.Configuration.GetValue<string>("SqlConnectionString")
    ?? Environment.GetEnvironmentVariable("SqlConnectionString");

builder.Services.AddDbContext<RunDbContext>(options =>
    options.UseSqlServer(connectionString));

// Application Insights commented out here
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();