using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public class ExtensionsConfigurationDataSource
    {
        private static ConcurrentDictionary<string, object> _extensiosConfigs = new ConcurrentDictionary<string, object>();

        public static object Register(string section, object config)
        {
           return _extensiosConfigs.AddOrUpdate(section, config, (k, v) => v);
        }

        public static void Clear()
        {
            _extensiosConfigs.Clear();
        }

        public static JObject GetJson()
        {
            var json = new JObject();
            foreach(var kv in _extensiosConfigs)
            {
                json.Add(kv.Key, JObject.Parse(JsonConvert.SerializeObject(kv.Value)));
            }
            return json;
        }

    }
}
