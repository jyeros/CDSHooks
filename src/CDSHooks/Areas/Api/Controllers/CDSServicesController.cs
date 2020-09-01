using CDSHooks.Core;
using CDSHooks.Core.Models;
using CDSHooks.Data.DBContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CDSHooks.Areas.Api.Controllers
{
    [Route("cds-services")]
    [ApiController]
    public class CDSServicesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDispatchExecuteService dispatchExecuteService;

        public CDSServicesController(ApplicationDbContext dbContext, IDispatchExecuteService dispatchExecuteService)
        {
            this.dbContext = dbContext;
            this.dispatchExecuteService = dispatchExecuteService;
        }

        // GET: cds-services
        [HttpGet]
        public IActionResult Discovery()
        {
            var services = dbContext.Services.ToList().Select(x => x.ToDiscoveryViewModel());

            return new JsonResult(new { services });
        }

        [HttpPost("{id}")]
        public IActionResult ExecuteService(string id, [FromBody] ExecuteServiceRequest executeService)
        {
            var service = dbContext.Services.Include(s => s.Hook).SingleOrDefault(x => x.Id == id);
            if (service == null)
                return NotFound();

            var result = dispatchExecuteService.Dispatch(executeService, service);

            return new JsonResult(result);
        }

    }
}
