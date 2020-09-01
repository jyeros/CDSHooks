using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Domain
{
    public class CDSService
    {
        [Required]
        public string HookId { get; set; }
        public Hook Hook { get; set; }
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Id { get; set; }
        // TODO: value should be restricted to fhir read or search
        public IDictionary<string, string> Prefetch { get; set; }
        [Required]
        public CDSServiceCodeType CodeType { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
