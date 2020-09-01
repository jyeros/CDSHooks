using CDSHooks.Core;
using CDSHooks.Core.Models;
using CDSHooks.Data.DBContexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CDSHooks.Areas.Api.Controllers
{
    [Route("cds-services")]
    [ApiController]
    public class CDSServicesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CDSServicesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: cds-services
        [HttpGet]
        public IActionResult Discovery()
        {
            var services = dbContext.Services.ToList().Select(x => x.ToDiscoveryViewModel());

            return new JsonResult(new { services });
        }

    }
}
