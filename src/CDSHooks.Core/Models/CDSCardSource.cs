using Hl7.Fhir.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Core.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CDSCardSource
    {
        [Required]
        public string Label { get; set; }
        // TODO: should be absolute url
        public string Url { get; set; }
        // TODO: should be absolute url
        public string Icon { get; set; }
        public Coding Topic { get; set; }
    }
}
