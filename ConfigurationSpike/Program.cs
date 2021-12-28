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
            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(
            services =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    // this section has not called yet, when executed when we call AddHostedService
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    IConfigurationRoot configurationRoot = config.Build();
                    var extensionsSection = configurationRoot.GetSection("extensions");
                    var kafkaSection = extensionsSection.GetSection("kafka");
                    KafkaOptions kafkaOptions = new();
                    kafkaSection.Bind(kafkaOptions);
                    services.AddSingleton<HostConfigService>(provider =>
                    {
                        var service = provider.GetRequiredService<HostConfigService>();
                        if (service == null)
                            service = new HostConfigService();
                        service.Content.Add("kafka", JObject.Parse(JsonConvert.SerializeObject(kafkaOptions)));
                        return service;
                    });
                    var httpSection = extensionsSection.GetSection("http");
                    HttpOptions httpOptions = new();
                    httpSection.Bind(httpOptions);
                    services.AddSingleton<HostConfigService>(provider =>
                    {
                        var service = provider.GetRequiredService<HostConfigService>();
                        service.Content.Add("http", JObject.Parse(JsonConvert.SerializeObject(httpOptions)));
                        return service;
                    });
                });
            }).ConfigureServices(services =>
            {
                services.AddHostedService<FunctionsHostService>();
            });
            return builder;
        }
                
    }
}