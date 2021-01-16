using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.School.Areas.Class.Services;
using ExplyMe.Modules.School.Areas.Class.Services.Interfaces;
using ExplyMe.Modules.School.Areas.School.Services;
using ExplyMe.Modules.School.Areas.School.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExplyMe.Modules.School
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISchoolCreator, SchoolCreator>();
            services.AddTransient<ISchoolFinder, SchoolFinder>();
            services.AddTransient<IClassCreator, ClassCreator>();
        }
    }
}
