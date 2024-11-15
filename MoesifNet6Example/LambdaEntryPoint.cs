#define MOESIF_INSTRUMENT

using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
// using System.Collections.Generic;
// using System.Text.Json;
// using System;
// using System.Net;

#if MOESIF_INSTRUMENT
using System.Diagnostics;
#endif



namespace MoesifNet6Example
{

    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        private readonly ILogger<LambdaEntryPoint> _logger;
#if MOESIF_INSTRUMENT
        private static int invovationCount = 0;
#endif
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
#if MOESIF_INSTRUMENT
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long dbgTime = 0;

            _logger.LogInformation($"Enter handler FunctionHandlerAsync Iteration - {invovationCount}");
            invovationCount += 1;

            // _logger.LogInformation(request.Path);
            // stopwatch.Start();
            // _logger.LogInformation("Inside in the async before base - ");
#endif
            var response = await base.FunctionHandlerAsync(request, lambdaContext);

#if MOESIF_INSTRUMENT
            //  stopwatch.Stop();

            //  var response = new APIGatewayProxyResponse
            // {
            //     StatusCode = 200,
            //     Body = "{\"message\":\"Lambda is working! Response logged.\"}",
            //     Headers = new Dictionary<string, string>
            //     {
            //         { "Content-Type", "application/json" }
            //     }
            // };
        
            // Get the elapsed time in milliseconds
            // long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            
            // _logger.LogInformation($"FunctionHandlerAsync execution time: {elapsedMilliseconds} milliseconds");

            // _logger.LogInformation("response in the async after base - ");
            // _logger.LogInformation(response.Body);
            // _logger.LogInformation(response.StatusCode.ToString());

            // Log the response body to Moesif
            // response.Body = "{\"message\":\"Lambda is working! Response logged.\"}";

            // var response = new APIGatewayProxyResponse
            // {
            //     StatusCode = 200,
            //     Body = "{\"message\":\"Lambda is working! Response logged.\"}",
            //     Headers = new Dictionary<string, string>
            //     {
            //         { "Content-Type", "application/json" }
            //     }
            // };

            dbgTime = stopwatch.ElapsedMilliseconds;
            _logger.LogInformation($"EXITING handler FunctionHandlerAsync ExecutionTime - {dbgTime}ms", dbgTime);
            stopwatch.Stop();
#endif
            return response;
        }
    }
}

