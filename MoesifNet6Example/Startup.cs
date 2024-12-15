#define MOESIF_INSTRUMENT

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Nodes;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moesif.Middleware;
using MoesifNet6Example.Settings;
using Microsoft.AspNetCore.Routing;

namespace MoesifNet6Example
{
    public class Startup
    {
        public static int counter = 0;
        public IConfiguration Configuration { get; private set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            counter += 1;
#if MOESIF_INSTRUMENT
            Console.WriteLine($"Begin: Configure");
#endif
            var isLambda = false;
            var isMoesifEnabled = true;
            var msg = "";
            MoesifOptions mo = new MoesifOptions(Configuration);
            ensureValidConfig(mo);
            
            isLambda = mo.IsLambda();
            isMoesifEnabled = mo.IsMoesifEnabled();
            if (isMoesifEnabled)
            {
                app.UseMiddleware<MoesifMiddleware>(mo.getMoesifOptions());
                msg = $"++++++ Moesif is Enabled because [IsMoesifEnabled = {isMoesifEnabled}] and [IsLambda = {isLambda}]";
            }
            else
            {
                msg = $"++++++ Moesif is Disabled because [IsMoesifEnabled = {isMoesifEnabled}] and [IsLambda = {isLambda}]";
            }
            Console.WriteLine($"{msg}");
            string isoUtcDateString = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Console.WriteLine($"[{isoUtcDateString}] **** Moesif-INIT = [{counter}]");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
#if MOESIF_INSTRUMENT
            Console.WriteLine($"Before: Configure / UseRouting");
#endif
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var msg = $"Hello World! isLambda = {isLambda}"; 
                    Console.WriteLine($"Hit the home page: {msg}");
                    // if (isLambda)
                    // {
                    //     var response = new APIGatewayProxyResponse
                    //     {
                    //         StatusCode = (int)HttpStatusCode.OK,
                    //         Body = $"Welcome to Home! Request path: /",
                    //         Headers = new Dictionary<string, string> { { "Content-Type", "text/json" } }
                    //     };
                    // }
                    // else
                    // {
                    await context.Response.WriteAsync(new JsonObject
                    {
                        ["msg"] = msg,
                        ["zipcode"] = 94709
                    }.ToString());
                    // }
                });

                endpoints.MapGet("/foo", async context =>
                {
                    // var hdrs = new Dictionary<string, string> { { "Content-Type", "text/json" } };
                    var response = new JsonObject
                    {
                        ["StatusCode"] = (int)HttpStatusCode.OK,
                        ["Body"] = $"Hello from Lambda! Request path: /foo"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                });

                endpoints.MapControllerRoute(
                name: "api",
                pattern: "{controller}/{id}");
            });

#if MOESIF_INSTRUMENT
            Console.WriteLine($"End: Configure");
#endif
        }

        private static void ensureValidConfig(MoesifOptions mo)
        {
            if (!mo.isConfiguredMoesifApplicationId())
            {
                string msg = "Error: Moesif: " + MoesifOptionsParamNames.ApplicationId + " not valid - usually in appsettings(.env).json";
                Console.WriteLine(msg);
                throw new ArgumentException(msg);
            }
            else
            {
                Console.WriteLine("MoesifOptions");
                // PrintDictionaries(mo);
            }
        }

        private static void PrintDictionaries(MoesifOptions mo)
        {
            foreach (var kvp in mo.getMoesifOptions())
            {
                Console.WriteLine($"Key = {kvp.Key}, Value = {kvp.Value.ToString()}");
            }
        }
    }
}

