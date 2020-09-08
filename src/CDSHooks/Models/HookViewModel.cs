using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

    public class HookContextViewModel : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();

            if (!Constants.allowedPrefetchTokenTypes.Contains(System.Type.GetType(Type)))
            {
                var allowedPrefetchTypesDisplay = Constants.allowedPrefetchTokenTypes.Select(t => Constants.allowedTypes[t.FullName]);
                validationResult.Add(
                    new ValidationResult(
                        $"Type {Constants.allowedTypes[Type]} can't be prefetch token, only {string.Join(", ", allowedPrefetchTypesDisplay)} are allowed.",
                        new[] { nameof(IsPrefetchToken) })
                    );
            }

            return validationResult;
        }
    }
}
