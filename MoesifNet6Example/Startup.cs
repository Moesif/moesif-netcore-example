using System;
using System.Collections.Generic;
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
            MoesifOptions mo = new MoesifOptions(Configuration);
            ensureValidConfig(mo);
            app.UseMiddleware<MoesifMiddleware>(mo.getMoesifOptions());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapControllerRoute(
                name: "api",
                pattern: "{controller}/{id}");
            });
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

