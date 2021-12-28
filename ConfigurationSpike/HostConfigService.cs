using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public interface IHostConfigService
    {
        JObject Content { get; set; }
    }
    public class HostConfigService
    {
        public JObject Content { get; set; } = new JObject();
    }
}
