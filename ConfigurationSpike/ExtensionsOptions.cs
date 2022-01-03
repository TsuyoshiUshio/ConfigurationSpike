using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public class ExtensionsOptions
    {
        public HttpOptions http { get; set; }
        public KafkaOptions kafka { get; set; }
        public EventHubsOptions eventHubs { get; set; }
    }
}
