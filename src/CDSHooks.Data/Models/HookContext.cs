using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Data.Models
{
    public class HookContext
    {
        [Required]
        public string Field { get; set; }
        [Required]
        public ContextOptionality Optionality { get; set; }
        [Required]
        public bool IsPrefetchToken { get; set; }
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
