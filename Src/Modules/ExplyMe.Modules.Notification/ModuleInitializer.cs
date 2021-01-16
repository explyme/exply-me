using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.Notification.Data;
using ExplyMe.Modules.Notification.Data.Interface;
using ExplyMe.Modules.Notification.Services;
using ExplyMe.Modules.Notification.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExplyMe.Modules.Notification
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<INotificationTypeRepository, NotificationTypeRepository>();
            services.AddTransient<INotificationMessageCompiler, NotificationMessageCompiler>();
        }
    }
}
