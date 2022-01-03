using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public class ExtensionsOptions
    {
        public HttpOptions Http { get; set; }
        public KafkaOptions Kafka { get; set; }
        public EventHubsOptions EventHubs { get; set; }
    }
}
