using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Wallet.Data.Interfaces;
using ExplyMe.Modules.Wallet.Domain;
using ExplyMe.Modules.Wallet.Domain.Entities;
using ExplyMe.Modules.Wallet.Domain.Enums;
using ExplyMe.Modules.Wallet.Services.Interfaces;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services
{
    public class WalletTransferService : IWalletTransferService
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public IWalletAccountBalanceStore WalletAccountBalanceStore { get; }
        public IWalletTransactionStore WalletTransactionStore { get; }

        public WalletTransferService(
            ISqlConnectionFactory sqlConnectionFactory,
            IWalletAccountBalanceStore walletAccountBalanceStore,
            IWalletTransactionStore walletTransactionStore)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
            WalletAccountBalanceStore = walletAccountBalanceStore ?? throw new ArgumentNullException(nameof(walletAccountBalanceStore));
            WalletTransactionStore = walletTransactionStore ?? throw new ArgumentNullException(nameof(walletTransactionStore));
        }

        public async Task<WalletOperationResult> Transfer(
            long originAccount, 
            long destinationAccount, 
            long amount,
            string softDescriptor)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            await connection.OpenAsync();
            using var dbTransaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, CancellationToken.None);

            // Debita o valor do saldo disponível da conta do depositante
            await WalletAccountBalanceStore.DecrementAvailableBalanceAsync(amount, originAccount, connection, dbTransaction);

            // Credita o valor na agente bloqueada do depositante
            await WalletAccountBalanceStore.IncrementBlockedBalanceAsync(amount, originAccount, connection, dbTransaction);

            // Credita o valor na agenda futura do recebedor
            await WalletAccountBalanceStore.IncrementFutureBalanceAsync(amount, destinationAccount, connection, dbTransaction);

            var walletTransaction = new WalletTransaction
            {
                Status = TransactionStatusEnum.Pending,
                Amount = amount,
                FromId = originAccount,
                ToId = destinationAccount,
                SoftDescriptor = softDescriptor
            };

            // Insere a operação pendente no banco
            await WalletTransactionStore.CreateAsync(walletTransaction, connection, dbTransaction);
            await dbTransaction.CommitAsync();

            return WalletOperationResult.Ok(walletTransaction.Id);
        }

        public Task<WalletOperationResult> Execute(Guid transactionId)
        {
            return Task.FromResult<WalletOperationResult>(null);
        }

        public Task<WalletOperationResult> Void(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
