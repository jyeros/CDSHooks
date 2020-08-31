using Microsoft.EntityFrameworkCore;

namespace CDSHooks.Data.DBContexts
{
    public class ApplicationDbContext : CDSHookModelsDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
