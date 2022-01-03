// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConfigurationSpike
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
          //  var theOtherApproachOfArgs = Environment.GetCommandLineArgs();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            ExtensionsConfigurationDataSource.Clear();
            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configurationRoot = config.Build();
                var extensionsSection = configurationRoot.GetSection("extensions");
                var kafkaSection = extensionsSection.GetSection("kafka");
                KafkaOptions kafkaOptions = new();
                kafkaSection.Bind(kafkaOptions);
                ExtensionsConfigurationDataSource.Register("kafka", kafkaOptions);
            }).ConfigureAppConfiguration((context, config)=> {
                IConfigurationRoot configurationRoot = config.Build();
                var extensionsSection = configurationRoot.GetSection("extensions");
                var httpSection = extensionsSection.GetSection("http");
                HttpOptions httpOptions = new();
                httpSection.Bind(httpOptions);
                ExtensionsConfigurationDataSource.Register("http", httpOptions);
            })
            .ConfigureServices((context, services) => {
                var configurationRoot = context.Configuration;
                services.Configure<KafkaOptions>(
                    configurationRoot.GetSection("extensions").GetSection("kafka")
                    );
                services.Configure<HttpOptions>(
                    configurationRoot.GetSection("extensions:http")
                    );
                services.Configure<ExtensionsOptions>(
                    configurationRoot.GetSection(Constants.ExtensionsConfigurationKey));
            })
            .ConfigureAppConfiguration(config =>
            {
                config.AddExtensionsConfigration();
            })
            .ConfigureServices(services =>
            {
                services.AddHostedService<FunctionsHostService>();
            });
            
            return builder;
        }
                
    }
}