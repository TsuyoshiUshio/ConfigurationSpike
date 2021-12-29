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
        public override void Load()
        {
            Data.Add(Constants.ExtensionsConfigurationKey, JsonConvert.SerializeObject(ExtensionsConfigurationDataSource.GetJson()));
        }
    }
}
