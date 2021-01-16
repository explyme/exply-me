using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.Wallet.Services;
using ExplyMe.Modules.Wallet.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ExplyMe.Modules.Wallet
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IWalletAccountService, WalletAccountService>();

            services.AddTransient<IWalletTransferService, WalletTransferService>();
        }
    }
}
