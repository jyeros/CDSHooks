using CDSHooks.Core.Models;
using CDSHooks.Domain;

namespace CDSHooks.Core
{
    public static class ModelExtensions
    {
        public static CDSServiceDiscoveryViewModel ToDiscoveryViewModel(this CDSService service)
        {
            return new CDSServiceDiscoveryViewModel
            {
                Description = service.Description,
                Hook = service.HookId,
                Id = service.Id,
                Prefetch = service.Prefetch,
                Title = service.Title
            };
        }
    }
}
