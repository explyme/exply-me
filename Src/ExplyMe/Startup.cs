using ExplyMe.DependencyInjection;
using ExplyMe.Extensions;
using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.ChatMessaging;
using ExplyMe.Infrastructure.Services;
using ExplyMe.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExplyMe.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddInfrastructureModule(Configuration);

            services.AddAuthorization();
            services.AddCustomizedIdentity();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.ConsentCookie.IsEssential = true;
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMemoryCache();
            services.AddSingleton<IAppSettingsService, AppSettingsService>();

            ModuleInjector
                .CreateInjector()
                .Inject<Modules.Core.ModuleInitializer>()
                .Inject<Modules.Notification.ModuleInitializer>()
                .Inject<Modules.Wallet.ModuleInitializer>()
                .Inject<Modules.Call.ModuleInitializer>()
                .Inject<Modules.ChatMessaging.ModuleInitializer>()
                .Inject<Modules.School.ModuleInitializer>()
                .BindServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapFallbackToPage("/_Host");

            });

            ModuleInjector.ConfigureServices(app, env);
        }
    }
}
