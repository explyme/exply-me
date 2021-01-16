using ExplyMe.Modules.Wallet.Domain;
using ExplyMe.Modules.Wallet.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services.Interfaces
{
    public interface IWalletAccountService
    {
        Task<WalletAccount> CreateAsync(long userId);
        Task<IEnumerable<WalletAccount>> FindUserAccountsAsync(long userId);
        Task<WalletAccount> GetAsync(long accountId);
        Task<WalletAccountBalance> GetTotalBalance(long accountId);
        Task<bool> AuditBalance(long accountId);
    }
}
