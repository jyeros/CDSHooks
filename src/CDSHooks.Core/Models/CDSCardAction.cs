using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Core.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CDSCardAction
    {
        [Required]
        //TODO: Enum should be string with camelCase
        public CDSCardActionType Type { get; set; }
        [Required]
        public string Description { get; set; }
        // TODO: string or Fhir.Resource
        public object Resource { get; set; }
    }
}
