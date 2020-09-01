using CDSHooks.Data.DBContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace CDSHooks.Core
{
    public static class CoreExtensions
    {
        public static void InitializeCDSHooksServerDatabase(this IApplicationBuilder app, IHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                if (env.IsDevelopment())
                {
                    if (!context.Hook.Any())
                    {
                        foreach (var hook in Config.GetHooks())
                        {
                            context.Hook.Add(hook);
                        }
                        context.SaveChanges();
                    }
                    if (!context.Services.Any())
                    {
                        foreach (var service in Config.GetServices())
                        {
                            context.Services.Add(service);
                        }
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
