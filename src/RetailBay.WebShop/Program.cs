using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace RetailBay.WebShop
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ConfigureSerilog();

            var host = CreateWebHostBuilder(args)
                .UseSerilog()
                .Build();

            try
            {
                Log.Information("Starting web host");
                host.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hosigngContext, config) =>
                {
                    // This will disable default Microsoft logger.
                    config.ClearProviders();
                })
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

        public static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "WebShop")
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://retailbay_elasticsearch:9200/"))
                {
                    AutoRegisterTemplate = true
                })
            .CreateLogger();
        }
    }
}
