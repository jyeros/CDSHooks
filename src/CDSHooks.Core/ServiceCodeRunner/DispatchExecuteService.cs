using CDSHooks.Core.Models;
using CDSHooks.Domain;
using Hl7.Fhir.Model;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CDSHooks.Core.ServiceCodeRunner
{
    public class DispatchExecuteService : IDispatchExecuteService
    {
        public async Task<ExecuteServiceResponse> Dispatch(ExecuteServiceRequest executeService, CDSService service)
        {
            return service.CodeType switch
            {
                CDSServiceCodeType.JSON => JsonConvert.DeserializeObject<ExecuteServiceResponse>(service.Code),
                CDSServiceCodeType.CSharp => await ServiceCSharpCodeRunner.Evaluate(service.Code,
                    executeService.Context, executeService.Prefetch),
                _ => throw new System.NotImplementedException()
            };
        }
    }
}
