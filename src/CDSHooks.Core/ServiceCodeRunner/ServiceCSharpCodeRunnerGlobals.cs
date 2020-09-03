using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace CDSHooks.Core.ServiceCodeRunner
{
    public class ServiceCSharpCodeRunnerGlobals
    {
        public IDictionary<string, object> context { get; set; }
        public IDictionary<string, Resource> prefetch { get; set; }
    }
}
