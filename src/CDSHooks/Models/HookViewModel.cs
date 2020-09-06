using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Models
{
    public class HookViewModel
    {
        [Required]
        public string Id { get; set; }
        public string Workflow { get; set; }
        [Required]
        public IList<HookContextViewModel> Context { get; set; }
    }

    public class HookContextViewModel
    {
        [Required]
        public string Field { get; set; }
        [Required]
        [DisplayName("Required")]
        public bool IsRequired { get; set; }
        [Required]
        [DisplayName("Prefetch Token")]
        public bool IsPrefetchToken { get; set; }
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
