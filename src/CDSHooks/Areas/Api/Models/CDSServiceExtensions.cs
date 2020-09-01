using CDSHooks.Domain;

namespace CDSHooks.Areas.Api.Models
{
    public static class CDSServiceExtensions
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
