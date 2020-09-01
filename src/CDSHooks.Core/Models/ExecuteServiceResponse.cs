using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace CDSHooks.Core.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ExecuteServiceResponse
    {
        public IEnumerable<CDSCard> Cards { get; set; }
        public IEnumerable<CDSCardAction> SystemActions { get; set; }
    }
}
