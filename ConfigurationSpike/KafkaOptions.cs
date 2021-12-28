using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public class KafkaOptions
    {
        public int MaxPollingInterval { get; set; }
        public int AutoCommitInterval { get; set; } = 3;
    }
}
