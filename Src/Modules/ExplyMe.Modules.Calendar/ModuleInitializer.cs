using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.Calendar.Store;
using ExplyMe.Modules.Calendar.Store.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExplyMe.Modules.Calendar
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICalendarSlotStore, CalendarSlotStore>();
        }
    }
}
