using CDSHooks.Data.Models;
using CDSHooks.Data.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CDSHooks.Data.DBContexts
{
    public class CDSHookModelsDbContext : DbContext
    {
        public CDSHookModelsDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Hook> Hook { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HookConfiguration());
        }
    }
}
