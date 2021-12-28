using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    internal class FunctionsHostService : IHostedService
    {
        private HostConfigService _hostConfigService;
        private KafkaOptions _kafkaOptions;
        private HttpOptions _httpOptions;
        public FunctionsHostService(HostConfigService hostConfigService, KafkaOptions kafkaOptions, HttpOptions httpOptions)
        {
            _hostConfigService = hostConfigService;
            _kafkaOptions = kafkaOptions;
            _httpOptions = httpOptions;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("HostConfigService:");
            Console.WriteLine(JsonConvert.SerializeObject(_hostConfigService));
            Console.WriteLine("KafkaOptions:");
            Console.WriteLine(JsonConvert.SerializeObject(_kafkaOptions));
            Console.WriteLine("HttpOptions:");
            Console.WriteLine(JsonConvert.SerializeObject(_httpOptions));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Drain Mode started...");
            return Task.CompletedTask;
        }
    }
}
