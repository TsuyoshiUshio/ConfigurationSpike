using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSpike
{
    public class ExtensionsConfigurationProvider : ConfigurationProvider
    {
        private Stack<string> _path;
        public ExtensionsConfigurationProvider()
        {
            _path = new Stack<string>();
            ExtensionsConfigurationDataSource.Subscribe(nameof(ExtensionsConfigurationProvider), Load);
        }
        
        public override void Load()
        {
            _path = new Stack<string>();
            if (Data.ContainsKey(Constants.ExtensionsConfigurationKey)) {
                Data.Remove(Constants.ExtensionsConfigurationKey);
            }
            var json = ExtensionsConfigurationDataSource.GetJson();
            ProcessObject(json);
            OnReload();
        }

        private void ProcessObject(JObject json)
        {
            foreach(var property in json.Properties())
            {
                _path.Push(property.Name);
                ProcessProperty(property);
                _path.Pop();
            }
        }

        private void ProcessProperty(JProperty property)
        {
            ProcessToken(property.Value);
        }

        private void ProcessToken(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    ProcessObject(token.Value<JObject>());
                    break;
                case JTokenType.Array:
                    ProcessArray(token.Value<JArray>());
                    break;

                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.String:
                case JTokenType.Boolean:
                case JTokenType.Null:
                case JTokenType.Date:
                case JTokenType.Raw:
                case JTokenType.Bytes:
                case JTokenType.TimeSpan:
                    string key = Constants.ExtensionsConfigurationKey + ConfigurationPath.KeyDelimiter + ConfigurationPath.Combine(_path.Reverse());
                    Data[key] = token.Value<JValue>().ToString(CultureInfo.InvariantCulture);
                    break;
                default:
                    break;
            }
        }

        private void ProcessArray(JArray array)
        {
            for(int i = 0; i < array.Count(); i++)
            {
                _path.Push(i.ToString());
                ProcessToken(array[i]);
                _path.Pop();
            }
        }
    }
}
