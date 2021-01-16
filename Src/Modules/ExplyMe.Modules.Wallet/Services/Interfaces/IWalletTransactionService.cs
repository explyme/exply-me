using ExplyMe.Modules.Wallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services.Interfaces
{
    public interface IWalletTransactionService
    {
        Task<WalletTransaction> GetAsync(Guid transactionId);
        Task<IEnumerable<WalletTransaction>> FindByAccountAsync(long accoutnId);
        Task DepositAsync(long accountId, long amount);
    }
}
