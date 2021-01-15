using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExplyMe.Infrastructure.Modules
{
    public class ModuleInjector
    {
        public static IList<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();
        public static IList<Assembly> AllModulesAssembly => Modules.Select(x => x.Assembly).ToList();

        protected ModuleInjector()
        { }

        public static ModuleInjector CreateInjector() => new ModuleInjector();

        public ModuleInjector Inject<TModule>() => Inject<TModule>(nameof(TModule));

        public ModuleInjector Inject<TModule>(string name)
        {
            var moduleInfo = new ModuleInfo
            {
                Assembly = typeof(TModule).Assembly,
                Name = name
            };

            Modules.Add(moduleInfo);

            return this;
        }

        public void BindServices(IServiceCollection services)
        {
            foreach (var module in Modules)
            {
                var moduleInitializerType = module.Assembly.GetTypes().FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t));

                if (moduleInitializerType is null)
                    throw new Exception($"The module {module.Name} needs a ModuleInitializer");

                var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
                services.AddSingleton(typeof(IModuleInitializer), moduleInitializer);
                moduleInitializer.ConfigureServices(services);
            }
        }

        public static void ConfigureServices(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            var moduleInitializers = app.ApplicationServices.GetServices<IModuleInitializer>();
            foreach (var moduleInitializer in moduleInitializers)
                moduleInitializer.Configure(app, env);
        }
    }
}
