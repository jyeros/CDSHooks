using Hl7.Fhir.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Core.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ExecuteServiceRequest
    {
        [Required]
        public string Hook { get; set; }
        [Required]
        public string HookInstance { get; set; }
        public string FhirServer { get; set; }
        public object FhirAuthorization { get; set; }
        [Required]
        public IDictionary<string, object> Context { get; set; }
        public IDictionary<string, Resource> Prefetch { get; set; }
    }
}
