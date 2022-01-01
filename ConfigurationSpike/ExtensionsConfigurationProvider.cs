using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public class ExtensionsConfigurationProvider : ConfigurationProvider
    {
        public ExtensionsConfigurationProvider()
        {
            ExtensionsConfigurationDataSource.Subscribe(nameof(ExtensionsConfigurationProvider), Load);
        }
        
        public override void Load()
        {
            if (Data.ContainsKey(Constants.ExtensionsConfigurationKey)) {
                Data.Remove(Constants.ExtensionsConfigurationKey);
            }
            Data.Add(Constants.ExtensionsConfigurationKey, JsonConvert.SerializeObject(ExtensionsConfigurationDataSource.GetJson()));
        }
    }
}
