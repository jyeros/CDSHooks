using CDSHooks.Domain;
using System.Linq;

namespace CDSHooks.Models.Mappings
{
    public static class ServiceMapping
    {
        public static ServiceViewModel ToViewModel(this CDSService service)
        {
            return new ServiceViewModel
            {
                Id = service.Id,
                HookId = service.HookId,
                Title = service.Title,
                Description = service.Description,
                Prefetch = service.Prefetch?.Select(x => new PrefetchViewModel
                {
                    Key = x.Key,
                    Query = x.Value
                })?.ToList(),
                CodeType = service.CodeType,
                Code = service.Code,
            };
        }

        public static CDSService ToEntity(this ServiceViewModel serviceViewModel)
        {
            return new CDSService
            {
                Id = serviceViewModel.Id,
                HookId = serviceViewModel.HookId,
                Title = serviceViewModel.Title,
                Description = serviceViewModel.Description,
                Prefetch = serviceViewModel.Prefetch?.ToDictionary(x => x.Key, x => x.Query),
                CodeType = serviceViewModel.CodeType,
                Code = serviceViewModel.Code,
            };
        }
    }
}
