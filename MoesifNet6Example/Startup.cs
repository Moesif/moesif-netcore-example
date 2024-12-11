using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moesif.Middleware;
using MoesifNet6Example.Settings;
using Microsoft.AspNetCore.Routing;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;


namespace MoesifNet6Example
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; private set; }
        
        private readonly ILogger<Startup> _logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Add console logging
            }).CreateLogger<Startup>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
            services.AddSingleton(Configuration);
            //services.AddMvc();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            MoesifOptions mo = new MoesifOptions(Configuration);
            ensureValidConfig(mo);
            app.UseMiddleware<MoesifMiddleware>(mo.getMoesifOptions());

            stopwatch.Stop();

            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            _logger.LogError($"Startup moesif execution time: {elapsedMilliseconds} milliseconds");


            // MoesifMiddleware.Init(mo.getMoesifOptions());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // MoesifOptions mo = new MoesifOptions(Configuration);
            // ensureValidConfig(mo);
            // app.UseMiddleware<MoesifMiddleware>(mo.getMoesifOptions());

            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapGet("/", () =>
                //{
                //    return Results.Json(new { message = "Lambda is working! Response logged." });
                //});

                endpoints.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

                endpoints.MapPost("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

                //endpoints.MapGet("/", async context =>
                //{

                //    Console.WriteLine("Inside root method - ");
                //    // await context.Response.WriteAsync("Hello World!");
                //      var response = new APIGatewayProxyResponse
                //         {

                //             StatusCode = 200,
                //             Body = "{\"message\":\"Lambda is working! Response logged.\"}",
                //             Headers = new Dictionary<string, string>
                //             {
                //                 { "Content-Type", "application/json" }
                //             }
                //         };
                //    //return response;

                //    await context.Response.WriteAsJsonAsync(response);

                //    //var response = new Dictionary<string, string> {
                //    //    { "message", "Lambda is working! Response logged !!!! " }
                //    //};

                //    //context.Response.StatusCode = 200;
                //    //// foreach (var header in response.Headers)
                //    //// {
                //    ////     context.Response.Headers[header.Key] = header.Value;
                //    //// }

                //    //context.Response.Headers["Content-Type"] = "application/json" ;

                //    // await context.Response.WriteAsync(response.Body);

                //    //await context.Response.WriteAsJsonAsync(response);

                //});

                endpoints.MapControllerRoute(
                name: "api",
                pattern: "api/{controller}"); ///{action=Index}/{id?}
            });
        }

        public static void ensureValidConfig(MoesifOptions mo)
        {
            if (!mo.isConfiguredMoesifApplicationId())
            {
                string msg = "Error: Moesif: " + MoesifOptionsParamNames.ApplicationId + " not valid - usually in appsettings(.env).json";
                Console.WriteLine(msg);
                throw new ArgumentException(msg);
            }
        }
    }
}
