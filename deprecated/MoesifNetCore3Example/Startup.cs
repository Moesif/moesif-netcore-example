using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moesif.Middleware;
using MoesifNetCore3Example.Settings;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace MoesifNetCore3Example
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddMvc();
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            MoesifOptions mo = new MoesifOptions(Configuration);
            ensureValidConfig(mo);
            app.UseMiddleware<MoesifMiddleware>(mo.getMoesifOptions());


            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ExceptionInfo()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });

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

        public static void ensureValidConfig(MoesifOptions mo)
        {
            if (!mo.isConfiguredMoesifApplicationId()){
                string msg = "Error: Moesif: " + MoesifOptionsParamNames.ApplicationId + " not valid - usually in appsettings(.env).json";
                Console.WriteLine(msg);
                throw new ArgumentException(msg);
            }
        }
    }
}
