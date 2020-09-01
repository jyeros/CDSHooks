using Hl7.Fhir.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Core.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CDSCard
    {
        [JsonProperty("uuid")]
        public string UUId { get; set; }
        [Required]
        public string Summary { get; set; }
        public string Detail { get; set; }
        [Required]
        public string Indicator { get; set; }
        [Required]
        public CDSCardSource Source { get; set; }
        public IEnumerable<CDSCardSuggestion> Suggestions { get; set; }
        public string SelectionBehavior { get; set; }
        public IEnumerable<Coding> OverrideReasons { get; set; }
        public IEnumerable<CDSCardLink> Links { get; set; }
    }
}
