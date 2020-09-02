using CDSHooks.Core.Models;
using CDSHooks.Domain;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDSHooks.Core.PrefetchTemplate
{
    public interface IPrefetchTemplateRender
    {
        Task<(OperationOutcome outcome, Dictionary<string, Resource> prefetch)>
            Resolve(IDictionary<string, Resource> prefetchProvided,
                IDictionary<string, string> prefetchToResolve, IDictionary<string, object> context,
                Dictionary<string, HookContext> hookContext,
                string fhirServer, ExecuteServiceRequestFhirAuthorization fhirAuthorization);
    }
}
