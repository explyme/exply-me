using ExplyMe.Modules.Wallet.Domain;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services.Interfaces
{
    public interface IWalletTransferService
    {
        Task<WalletOperationResult> Transfer(long fromAccount, long toAccount, long amount, string softDescriptor);
        Task<WalletOperationResult> Void(Guid transactionId);
        Task<WalletOperationResult> Execute(Guid transactionId);
    }
}
