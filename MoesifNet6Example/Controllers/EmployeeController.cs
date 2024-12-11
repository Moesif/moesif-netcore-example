using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using MoesifNet6Example.Models;
using Amazon.Lambda.APIGatewayEvents;
using Moesif.Middleware.Models;
using Amazon.Lambda.Core;
using System.Text.Json;
using Microsoft.Extensions.Logging;



namespace MoesifNet6Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var employee = new Employee()
            {
                ID = id,
                FirstName = "firstName",
                LastName = "lastName",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            return Ok(employee);

            //var response = new APIGatewayProxyResponse
            //{
            //    StatusCode = 200,
            //    Body = System.Text.Json.JsonSerializer.Serialize(new { message = "Hello from Lambda!" }),
            //    Headers = new Dictionary<string, string>
            //    {
            //        { "Content-Type", "application/json" }
            //    }
            //};

            ////return new OkObjectResult(response);
            //return response;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            Console.WriteLine("Post method called");

            return Ok(employee);
            //return Ok(new { message = "Employee created successfully!" }); // , employee 
        }

        //[HttpPost]
        //public ActionResult<APIGatewayProxyResponse> Post()
        //{
        //    try
        //    {
        //        var responseBody = new { message = "Hello from Lambda!" };

        //        var response = new APIGatewayProxyResponse
        //        {
        //            StatusCode = 201,
        //            Body = JsonSerializer.Serialize(responseBody),
        //            Headers = new Dictionary<string, string>
        //            {
        //                { "Content-Type", "application/json" },
        //                { "Access-Control-Allow-Origin", "*" },
        //                { "Access-Control-Allow-Methods", "POST,OPTIONS" },
        //                { "Access-Control-Allow-Headers", "Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token" }
        //            }
        //        };

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new APIGatewayProxyResponse
        //        {
        //            StatusCode = 500,
        //            Body = JsonSerializer.Serialize(new { error = ex.Message }),
        //            Headers = new Dictionary<string, string>
        //            {
        //                { "Content-Type", "application/json" }
        //            }
        //        });
        //    }
        //}


        //[HttpPost]
        //public async Task<APIGatewayProxyResponse> Post(APIGatewayProxyRequest request)
        //{
        //    if (string.IsNullOrEmpty(request.Body))
        //    {
        //        return new APIGatewayProxyResponse
        //        {
        //            StatusCode = 400,
        //            Body = JsonSerializer.Serialize(new { message = "Employee data is null." }),
        //            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        //        };
        //    }

        //    var employee = JsonSerializer.Deserialize<Employee>(request.Body);

        //    // Log the successful addition of the employee
        //    _logger.LogInformation($"Employee added successfully");

        //    return new APIGatewayProxyResponse
        //    {
        //        StatusCode = 201,
        //        Body = JsonSerializer.Serialize(employee),
        //        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        //    };
        //}

        //[HttpPost]
        //public APIGatewayProxyResponse Post(APIGatewayProxyRequest request) // [FromBody] Employee employee
        //{

        //    try
        //    {
        //        Console.WriteLine("Inside POST Request - ");
        //        _logger.LogInformation($"Inside POST Request logger:");
        //        //return Ok(employee);
        //        var responseBody = new { message = "Hello from Lambda!" };

        //        //context.Logger.LogError($"responseBody: {responseBody}");
        //        //context.Logger.LogInformation($"responseBody: {responseBody}");


        //        var response = new APIGatewayProxyResponse
        //        {
        //            StatusCode = 201,
        //            Body = JsonSerializer.Serialize(responseBody),
        //            Headers = new Dictionary<string, string>
        //            {
        //                { "Content-Type", "application/json" },
        //                { "Access-Control-Allow-Origin", "*" },
        //                { "Access-Control-Allow-Methods", "POST,OPTIONS" },
        //                { "Access-Control-Allow-Headers", "Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token" }
        //            },
        //            IsBase64Encoded = false
        //        };

        //        //context.Logger.LogError($"response: {response}");

        //        //return new OkObjectResult(response);
        //        return response;

        //    }
        //    catch (Exception ex)
        //    {
        //        //context.Logger.LogError($"Error: {ex.Message}");
        //        //context.Logger.LogInformation($"Error: {ex.Message}");
        //        return new APIGatewayProxyResponse
        //        {
        //            StatusCode = 500,
        //            Body = JsonSerializer.Serialize(new { error = ex.Message }),
        //            Headers = new Dictionary<string, string>
        //            {
        //                { "Content-Type", "application/json" }
        //            }
        //        };
        //    }
        //}

        //[HttpPost]
        //public APIGatewayProxyResponse Post([FromBody] Employee employee, ILambdaContext context)
        //{
        //    // Log the received employee data
        //    context.Logger.LogLine($"Received employee data for POST Body");

        //    // Check if the employee is null or invalid
        //    if (employee == null)
        //    {
        //        return new APIGatewayProxyResponse
        //        {
        //            StatusCode = 400,
        //            Body = JsonSerializer.Serialize(new { message = "Invalid employee data." }),
        //            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        //        };
        //    }

        //    // You can perform some processing here (e.g., store employee data in DB)

        //    // Prepare the response
        //    var response = new APIGatewayProxyResponse
        //    {
        //        StatusCode = 200,
        //        Body = JsonSerializer.Serialize(new { message = "Hello from Lambda!", employee = employee }),
        //        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        //    };

        //    return response;
        //}
    }
}
