using CDSHooks.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Models
{
    public class ServiceViewModel
    {
        [Required]
        [DisplayName("Hook id")]
        public string HookId { get; set; }
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Id { get; set; }
        [DisplayName("Prefetch data")]
        public IList<PrefetchViewModel> Prefetch { get; set; }
        [Required]
        [DisplayName("Code type")]
        public CDSServiceCodeType CodeType { get; set; }
        [Required]
        public string Code { get; set; }
    }

    public class PrefetchViewModel
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Query { get; set; }
    }
}
