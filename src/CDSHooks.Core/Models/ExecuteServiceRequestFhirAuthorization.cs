using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Core.Models
{
    public class ExecuteServiceRequestFhirAuthorization
    {
        [Required]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [Required]
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [Required]
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        [Required]
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [Required]
        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}