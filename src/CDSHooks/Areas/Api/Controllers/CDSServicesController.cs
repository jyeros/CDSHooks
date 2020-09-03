using CDSHooks.Core;
using CDSHooks.Core.Models;
using CDSHooks.Core.PrefetchTemplate;
using CDSHooks.Core.ServiceCodeRunner;
using CDSHooks.Data.DBContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CDSHooks.Areas.Api.Controllers
{
    [Route("cds-services")]
    [ApiController]
    public class CDSServicesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDispatchExecuteService dispatchExecuteService;
        private readonly IHookContextParser hookContextParser;
        private readonly IPrefetchTemplateRender prefetchResolver;

        public CDSServicesController(ApplicationDbContext dbContext, IDispatchExecuteService dispatchExecuteService,
            IHookContextParser hookContextParser, IPrefetchTemplateRender prefetchResolver)
        {
            this.dbContext = dbContext;
            this.dispatchExecuteService = dispatchExecuteService;
            this.hookContextParser = hookContextParser;
            this.prefetchResolver = prefetchResolver;
        }

        // GET: cds-services
        [HttpGet]
        public async Task<IActionResult> Discovery()
        {
            var services = (await dbContext.Services.ToListAsync()).Select(x => x.ToDiscoveryViewModel());

            return new JsonResult(new { services });
        }

        // POST: cds-services/{id}
        [HttpPost("{id}")]
        public async Task<IActionResult> ExecuteService(string id, [FromBody] ExecuteServiceRequest executeService)
        {
            var service = dbContext.Services.Include(s => s.Hook).SingleOrDefault(x => x.Id == id);
            if (service == null)
                return NotFound();

            var (outcomeContextParser, contextParsed) = hookContextParser.Parse(executeService.Context, service.Hook.Context);
            if (outcomeContextParser != null)
                return BadRequest(outcomeContextParser);

            var (outcomePrefetchResolved, prefetchResolved) = await prefetchResolver.Resolve(executeService.Prefetch, service.Prefetch,
                    contextParsed, service.Hook.Context.ToDictionary(x => x.Field), executeService.FhirServer,
                    executeService.FhirAuthorization);
            if (outcomePrefetchResolved != null)
                return BadRequest(outcomePrefetchResolved);

            executeService.Context = contextParsed;
            executeService.Prefetch = prefetchResolved;

            var result = await dispatchExecuteService.Dispatch(executeService, service);

            return new JsonResult(result);
        }
    }
}
