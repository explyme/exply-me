using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Wallet.Data.Interfaces;
using ExplyMe.Modules.Wallet.Domain.Entities;
using ExplyMe.Modules.Wallet.Domain.Enums;
using ExplyMe.Modules.Wallet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services
{
    public class WalletTransactionService : IWalletTransactionService
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public IWalletTransactionStore WalletTransactionStore { get; }
        public IWalletAccountBalanceStore WalletAccountBalanceStore { get; }

        public WalletTransactionService(
            ISqlConnectionFactory sqlConnectionFactory,
            IWalletTransactionStore walletTransactionStore,
            IWalletAccountBalanceStore walletAccountBalanceStore)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
            WalletTransactionStore = walletTransactionStore ?? throw new ArgumentNullException(nameof(walletTransactionStore));
            WalletAccountBalanceStore = walletAccountBalanceStore ?? throw new ArgumentNullException(nameof(walletAccountBalanceStore));
        }

        public async Task DepositAsync(long accountId, long amount)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            await connection.OpenAsync();
            using var dbTransaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, CancellationToken.None);

            var transaction = new WalletTransaction
            {
                Amount = amount,
                Status = TransactionStatusEnum.Executed,
                ToId = accountId,
                SoftDescriptor = "Depósito recebido"
            };

            await WalletTransactionStore.CreateAsync(transaction, connection, dbTransaction);
            await WalletAccountBalanceStore.IncrementAvailableBalanceAsync(accountId, amount, connection, dbTransaction);
            await dbTransaction.CommitAsync();
        }

        public async Task<IEnumerable<WalletTransaction>> FindByAccountAsync(long walletId)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            await connection.OpenAsync();
            using var dbTransaction = await connection.BeginTransactionAsync(IsolationLevel.ReadUncommitted, CancellationToken.None);

            var query = @"SELECT * FROM [WalletTransaction] WHERE [WalletTransaction].[ToId] = @walletId OR [WalletTransaction].[FromId] = @walletId";

            var result = await connection.QueryAsync<WalletTransaction>(query, new { walletId }, transaction: dbTransaction);

            return result;
        }

        public Task<WalletTransaction> GetAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
