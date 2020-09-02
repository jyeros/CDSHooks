using CDSHooks.Domain;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace CDSHooks.Core
{
    class HookContextParser : IHookContextParser
    {
        public (OperationOutcome outcome, IDictionary<string, object> context) 
            Parse(IDictionary<string, object> context, IList<HookContext> contextsSchema)
        {
            var contextParsed = new Dictionary<string, object>();
            var contextClone = new Dictionary<string, object>(context);
            var errors = new List<OperationOutcome.IssueComponent>();

            foreach (var item in contextsSchema)
            {
                if (contextClone.TryGetValue(item.Field, out var value))
                {
                    var v = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), item.Type);
                    contextParsed.Add(item.Field, v);
                    contextClone.Remove(item.Field);
                }
                else if (item.Optionality == ContextOptionality.REQUIRED)
                {
                    errors.Add(new OperationOutcome.IssueComponent
                    {
                        Severity = OperationOutcome.IssueSeverity.Fatal,
                        Code = OperationOutcome.IssueType.Invalid,
                        Diagnostics = $"Property {item.Field} is required at context"
                    });
                }
            }

            if (contextClone.Count > 0)
            {
                errors.AddRange(contextClone.Select(item => new OperationOutcome.IssueComponent
                {
                    Severity = OperationOutcome.IssueSeverity.Fatal,
                    Code = OperationOutcome.IssueType.Invalid,
                    Diagnostics = $"Extra property: context.{item.Key}"
                }));
            }

            return (errors.Count > 0 ? new OperationOutcome { Issue = errors } : null, contextParsed);
        }
    }
}
