using CDSHooks.Core.Models;
using CDSHooks.Domain;
using System.Threading.Tasks;

namespace CDSHooks.Core.ServiceCodeRunner
{
    public interface IDispatchExecuteService
    {
        Task<ExecuteServiceResponse> Dispatch(ExecuteServiceRequest executeService, CDSService service);
    }
}
