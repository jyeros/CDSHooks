using System.ComponentModel.DataAnnotations;

namespace CDSHooks.Core.Models
{
    public class CDSCardLink
    {
        [Required]
        public string Label { get; set; }
        [Required]
        // TODO: Url
        public string Url { get; set; }
        // TODO: Absolute or smart
        public string Type { get; set; }
        public string AppContext { get; set; }
    }
}