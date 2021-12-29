using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ConfigurationSpike
{
    public class FunctionsHostService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly KafkaOptions _kafkaOptions;
        private readonly HttpOptions _httpOptions;
        public FunctionsHostService(
            IConfiguration configration, IOptions<KafkaOptions> kafkaOptions, IOptions<HttpOptions> httpOptions
            )
        {
            _configuration = configration;
            _kafkaOptions = kafkaOptions.Value;
            _httpOptions = httpOptions.Value;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("FunctionsHostService Started...");
            ShowConfigrations();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Drain Mode started...");
            return Task.CompletedTask;
        }

        private void ShowConfigrations()
        {
            Console.WriteLine("ExteionsConfiguration:");
            Console.WriteLine(_configuration[Constants.ExtensionsConfigurationKey]);
        }
    }
}
