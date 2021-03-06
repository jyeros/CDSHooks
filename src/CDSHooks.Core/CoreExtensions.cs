﻿using CDSHooks.Core.Fhir;
using CDSHooks.Core.PrefetchTemplate;
using CDSHooks.Core.ServiceCodeRunner;
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
                if (!context.Hooks.Any())
                {
                    foreach (var hook in Config.GetHooks())
                    {
                        context.Hooks.Add(hook);
                    }
                    context.SaveChanges();
                }
                if (env.IsDevelopment())
                {
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

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IHookContextParser, HookContextParser>();
            services.AddScoped<IDispatchExecuteService, DispatchExecuteService>();
            services.AddScoped<IPrefetchTemplateRender, PrefetchTemplateRender>();
            services.AddScoped<ITemplateLanguage, RegexTemplateLanguage>();
            services.AddScoped<IFhirClientFactory, FhirClientFactory>();

            return services;
        }
    }
}
