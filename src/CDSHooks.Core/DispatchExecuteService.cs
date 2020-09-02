using CDSHooks.Core.Models;
using CDSHooks.Domain;
using Newtonsoft.Json;

namespace CDSHooks.Core
{
    public class DispatchExecuteService : IDispatchExecuteService
    {
        public ExecuteServiceResponse Dispatch(ExecuteServiceRequest executeService, CDSService service)
        {
            return service.CodeType switch
            {
                CDSServiceCodeType.JSON => JsonConvert.DeserializeObject<ExecuteServiceResponse>(service.Code)
            };
        }
    }
}
