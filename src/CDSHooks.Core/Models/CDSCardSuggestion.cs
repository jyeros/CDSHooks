using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Core.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CDSCardSuggestion
    {
        [Required]
        public string Label { get; set; }
        [JsonProperty("uuid")]
        public string UUId { get; set; }
        public bool IsRecommended { get; set; }
        public IEnumerable<CDSCardAction> Actions { get; set; }
    }
}
