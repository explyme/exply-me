using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.Call.Data;
using ExplyMe.Modules.Call.Data.Interfaces;
using ExplyMe.Modules.Call.Services;
using ExplyMe.Modules.Call.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExplyMe.Modules.Call
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICallRoomStore, CallRoomStore>();

            services.AddTransient<IRoomCreator, RoomCreator>();
        }
    }
}
