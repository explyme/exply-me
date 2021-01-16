using ExplyMe.Modules.Wallet.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services.Interfaces
{
    public interface IWalletTransactionService
    {
        Task<WalletTransaction> GetAsync(long walletId);
        Task<IEnumerable<WalletTransaction>> FindAsync(long walletId);
        Task DepositAsync(long userId, long amount);
    }
}
