using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddExtensionsConfigration(this IConfigurationBuilder builder)
        {
            return builder.Add(new ExtensionsConfigurationSource());
        }
    }
}
