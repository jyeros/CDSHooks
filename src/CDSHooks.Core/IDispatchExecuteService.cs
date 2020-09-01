using CDSHooks.Core.Models;
using CDSHooks.Domain;

namespace CDSHooks.Core
{
    public interface IDispatchExecuteService
    {
        ExecuteServiceResponse Dispatch(ExecuteServiceRequest executeService, CDSService service);
    }
}
