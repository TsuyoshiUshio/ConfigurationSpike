using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public class EventHubsOptions
    {
        public int AutoCommitInterval { get; set; } = 2;
    }
}
