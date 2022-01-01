using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using System.Timers;

namespace ConfigurationSpike
{
    public class FunctionsHostService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<KafkaOptions> _kafkaOptions;
        private readonly HttpOptions _httpOptions;

        private System.Timers.Timer _timer;
        public FunctionsHostService(
            IConfiguration configration, IOptionsMonitor<KafkaOptions> kafkaOptions, IOptions<HttpOptions> httpOptions
            )
        {
            _configuration = configration;
            _kafkaOptions = kafkaOptions;
            kafkaOptions.OnChange((options) =>
            {
                Console.WriteLine($"Kafka Options changed: {JsonConvert.SerializeObject(options)}");
            });
            _httpOptions = httpOptions.Value;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("FunctionsHostService Started...");
            UpdateTheRegistration(); // Simulate the value is updated.
            setTimer();
            ShowConfigrations();
            return Task.CompletedTask;
        }

        private void setTimer()
        {
            _timer = new System.Timers.Timer(2000);
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            ShowKafkaOptions(e);
        }

        private void ShowKafkaOptions(ElapsedEventArgs e)
        {
            Console.WriteLine($"{e.SignalTime}: KafaOptions: {JsonConvert.SerializeObject(_kafkaOptions.CurrentValue)}");
        }

        private void UpdateTheRegistration()
        {
            ExtensionsConfigurationDataSource.Register("eventHubs", new EventHubsOptions());
            // The following code force reload the IConfiguration object.
            //if (_configuration != null)
            //{
            //    IConfigurationRoot root = _configuration as IConfigurationRoot;
            //    root.Reload();
            //}
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
