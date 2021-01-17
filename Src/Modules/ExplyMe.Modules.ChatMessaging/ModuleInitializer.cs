using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.ChatMessaging.Services;
using ExplyMe.Modules.ChatMessaging.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExplyMe.Modules.ChatMessaging
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMessageCreator, MessageCreator>();
            services.AddTransient<IMessageFinder, MessageFinder>();
        }
    }
}
