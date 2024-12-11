
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;
using System;
using System.Net;



namespace MoesifNet6Example
{

    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        private readonly ILogger<LambdaEntryPoint> _logger;

        private static int invovationCount = 0;

         public LambdaEntryPoint()
        {
            // Create a logger factory and create a logger instance
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Add console logging
            });
            _logger = loggerFactory.CreateLogger<LambdaEntryPoint>();
        }


        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>();

            // builder.UseKestrel(options =>
            // {
            //     options.AllowSynchronousIO = true; // Enable synchronous IO
            // }).UseStartup<Startup>();
        }

        public override async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
        {
            _logger.LogInformation($"Enter handler FunctionHandlerAsync- {invovationCount}");

            invovationCount += 1;

            // _logger.LogInformation(request.Path);

            //  Stopwatch stopwatch = new Stopwatch();

            // stopwatch.Start();

            // _logger.LogInformation("Inside in the async before base - ");

            //var response = await base.FunctionHandlerAsync(request, lambdaContext);

            try
            {
                // Call the base function handler
                var response = await base.FunctionHandlerAsync(request, lambdaContext);

                // Log the response for debugging
                _logger.LogInformation($"Response: StatusCode={response.StatusCode}, Body={response.Body}");

                return response;
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                _logger.LogError(ex, "An error occurred while processing the request.");

                // Return a 500 Internal Server Error response with the exception message
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = JsonSerializer.Serialize(new { error = "An internal server error occurred.", details = ex.Message }),
                    Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            }
                };
            }

            //  stopwatch.Stop();

            //var response = new APIGatewayProxyResponse
            //{
            //    StatusCode = 200,
            //    Body = "{\"message\":\"Lambda is working! Response logged.\"}",
            //    Headers = new Dictionary<string, string>
            //     {
            //         { "Content-Type", "application/json" }
            //     }
            //};

            // Get the elapsed time in milliseconds
            // long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // _logger.LogInformation($"FunctionHandlerAsync execution time: {elapsedMilliseconds} milliseconds");

            // _logger.LogInformation("response in the async after base - ");
            // _logger.LogInformation(response.Body);
            // _logger.LogInformation(response.StatusCode.ToString());

            // Log the response body to Moesif
            // response.Body = "{\"message\":\"Lambda is working! Response logged.\"}";

            //var response = new APIGatewayProxyResponse
            //{
            //    StatusCode = 200,
            //    Body = "{\"message\":\"Lambda is working! Response logged.\"}",
            //    Headers = new Dictionary<string, string>
            //     {
            //         { "Content-Type", "application/json" }
            //     }
            //};

            //_logger.LogInformation($"Enter handler FunctionHandlerAsync response -  {response.StatusCode}, {response.Body} ");

            //return response;
        }
    }
}

