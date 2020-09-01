using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace CDSHooks.Areas.Api.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CDSServiceDiscoveryViewModel
    {
        public string Hook { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public IDictionary<string, string> Prefetch { get; set; }
    }
}
