using CDSHooks.Data.ModelsConfiguration;
using CDSHooks.Domain;
using Microsoft.EntityFrameworkCore;

namespace CDSHooks.Data.DBContexts
{
    public class CDSHookModelsDbContext : DbContext
    {
        public CDSHookModelsDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Hook> Hook { get; set; }
        public virtual DbSet<CDSService> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HookConfiguration());
            builder.ApplyConfiguration(new CDSServiceConfiguration());
        }
    }
}
