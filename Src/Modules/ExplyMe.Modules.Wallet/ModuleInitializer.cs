using ExplyMe.Infrastructure.Modules;
using ExplyMe.Modules.Wallet.Data;
using ExplyMe.Modules.Wallet.Data.Interfaces;
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
            services.AddTransient<IWalletAccountBalanceStore, WalletAccountBalanceStore>();
            services.AddTransient<IWalletTransactionStore, WalletTransactionStore>();

            services.AddTransient<IWalletAccountService, WalletAccountService>();
            services.AddTransient<IWalletTransferService, WalletTransferService>();
            services.AddTransient<IWalletTransactionService, WalletTransactionService>();

        }
    }
}
