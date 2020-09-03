using CDSHooks.Core.Models;
using Hl7.Fhir.Model;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CDSHooks.Core.ServiceCodeRunner
{
    public static class ServiceCSharpCodeRunner
    {
        public static async Task<ExecuteServiceResponse> Evaluate(string code, IDictionary<string, object> context,
            IDictionary<string, Resource> prefetch)
        {
            return await CSharpScript.EvaluateAsync<ExecuteServiceResponse>(code,
                    globals: new ServiceCSharpCodeRunnerGlobals { context = context, prefetch = prefetch },
                    options: Microsoft.CodeAnalysis.Scripting.ScriptOptions.Default.AddReferences(
                        Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(x => x.FullName)
                            .Append(Assembly.GetExecutingAssembly().FullName))
                    );
        }
    }
}
