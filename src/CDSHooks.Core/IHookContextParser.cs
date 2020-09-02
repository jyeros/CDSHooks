using CDSHooks.Domain;
using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace CDSHooks.Core
{
    public interface IHookContextParser
    {
        (OperationOutcome outcome, IDictionary<string, object> context) 
            Parse(IDictionary<string, object> context, IList<HookContext> contextsSchema);
    }
}
