using MoesifNet7Example;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                .ConfigureKestrel((context, options) => {
                    // Incase if want to use options.AllowSynchronousIO = false; please refer to Student controller endpoints
                    options.AllowSynchronousIO = true;
                }).ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    //logging.Configure()
                    logging.AddConsole().SetMinimumLevel(LogLevel.Error); // Add console logging provider
                    logging.AddDebug();  // Add debug logging provider
                    logging.AddEventSourceLogger();
                    //logging.AddFile("logs/myapp.log");
                });
            });
}

