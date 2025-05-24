# REST Api with Azure Functions

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![Azure Functions](https://img.shields.io/badge/Azure--Functions-Isolated%20Worker-blue)
![EF Core](https://img.shields.io/badge/EF--Core-SQL%20Server-blue)

## Overview

This repository contains a project for the *Design Cloud Solutions* course in the .NET Cloud Developer program at *IT-Högskolan*. 
It features an Azure Functions REST API for managing running entries (`RunEntry`). 
The API supports HTTP GET and POST requests to retrieve and add run data, which is stored in a SQL Server database hosted in Azure. 
The function itself is also deployed and running in Azure. Built with .NET 8 and using Entity Framework Core.


## Features

- **GET /api/runs**: Retrieves all runs from the database;
- **POST /api/runs**: Adds a new run entry. The request body should contain a JSON object representing the run.

## Technologies Used

- Azure Functions (.NET isolated worker model);
- Entity Framework Core (EF Core) with SQL Server;
- .NET 8.0;
- Dependency Injection for DbContext and Logger;
- **Azure** – for hosting both the SQL Server database and the deployed Azure Function.

## Function Overview

- The function is triggered by HTTP requests on the route `/runs`;
- For GET requests, it returns a list of all runs;
- For POST requests, it reads the JSON body, deserializes it into a `RunEntry` model, assigns a new GUID as ID, saves it to the database, and returns the saved run entry.

## Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download);
- [Azure Functions Core Tools v4](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local#v2);
- SQL Server instance (local or Azure SQL);
- Visual Studio or VS Code;
- Azure account (for deploying and testing the function in the cloud).

### Configuration

- Configure your connection string in `local.settings.json` under `SqlConnectionString`;
- Make sure your database exists and migrations are applied (if using EF migrations).

## Example Requests

Below is a summary of the available API endpoints and how to call them either locally during development or on the deployed Azure Function

| HTTP Method | Endpoint      | Description                  |
|-------------|---------------|------------------------------|
| GET         | `/api/runs`   | Retrieve a list of all runs   |
| POST        | `/api/runs`   | Add a new run entry           |

---

- **GET all runs**

  - Locally (during development): `GET http://localhost:{port}/api/runs`
  
  or
  
  - Deployed Azure Function: `GET https://sararunning.azurewebsites.net/api/runs?code=FUNCTION_KEY`

- **POST new run**  
  *Request Body Example:*

  ```json
  {
    "date": "2025-05-11T00:00:00",
    "distanceKm": 42.74,
    "duration": "03:48:28",
    "paceMinPerKm": 5.34,
    "shoesUsed": "Saucony Triumph 22",
    "notes": "Copenhagen Marathon 2025"
  }
  ```
## Notes

- The function generates a new unique ID for each run on POST;
- Make sure `appsettings.json` or `local.settings.json` contains the correct connection string;
- The project uses dependency injection to inject the database context.
- The `FUNCTION_KEY` in the URL is your Azure Function key used for authentication. You can find it in the Azure Portal under your Function App → Functions → (your function) → "Get Function URL".
- ⚠️ This project was deployed to Azure Functions as part of a school assignment. The deployed version might no longer be available due to cost constraints.

  

