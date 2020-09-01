using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Domain
{
    public class HookContext
    {
        [Required]
        public string Field { get; set; }
        [Required]
        // TODO: maybe change value to other type not enum to allow extensibility
        public ContextOptionality Optionality { get; set; }
        [Required]
        public bool IsPrefetchToken { get; set; }
        [Required]
        // TODO: restrict type
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
