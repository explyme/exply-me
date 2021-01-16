using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Wallet.Domain;
using ExplyMe.Modules.Wallet.Domain.Entities;
using ExplyMe.Modules.Wallet.Domain.Enums;
using ExplyMe.Modules.Wallet.Services.Interfaces;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services
{
    public class WalletTransferService : IWalletTransferService
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public WalletTransferService(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        private async Task UpdateBalance(
            string balanceField,
            long accountId,
            long amountToBeCalculated,
            SqlConnection connection,
            DbTransaction transaction)
        {
            var userBalance = await connection.ExecuteScalarAsync<long>(
                    $"SELECT TOP 1 [{balanceField}] from [WalletAccount] where [WalletAccount].Id = @accountId",
                    new { accountId });

            var newBalance = userBalance + amountToBeCalculated;

            await connection.ExecuteAsync(
                    $"UPDATE [WalletAccount] SET [{balanceField}] = @newBalance where [WalletAccount].Id = @accountId",
                    new { newBalance }, transaction: transaction);
        }

        private async Task InsertOperationAsync(
            WalletTransaction walletTransaction,
            SqlConnection connection,
            DbTransaction transaction)
        {
            await connection.ExecuteAsync(
@"INSERT INTO [WalletTransaction] 
    ([Id], [Status], [FromId], [ToId], [CreatedAt], [ExecutedAt], [VoidedAt], [Amount], [SoftDescriptor])
VALUES
    (@Id], @Status, @FromId, @ToId, @CreatedAt, @ExecutedAt, @VoidedAt, @Amount, @SoftDescriptor])",
                    walletTransaction, transaction: transaction);
        }

        public async Task<WalletOperationResult> Transfer(
            long fromAccount, 
            long toAccount, 
            long amount,
            string softDescriptor)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, CancellationToken.None);

            // Debita o valor do saldo disponível da conta do depositante
            await UpdateBalance("AvailableBalance", amount * -1, fromAccount, connection, transaction);
            // Credita o valor na agente bloqueada do depositante
            await UpdateBalance("BlockedBalance", amount, fromAccount, connection, transaction);

            // Credita o valor na agenda futura do recebedor
            await UpdateBalance("FutureBalance", amount, toAccount, connection, transaction);

            var walletTransaction = new WalletTransaction
            {
                Status = TransactionStatusEnum.Pending,
                Amount = amount,
                FromId = fromAccount,
                ToId = toAccount,
                SoftDescriptor = softDescriptor
            };

            // Insere a operação pendente no banco
            await InsertOperationAsync(walletTransaction, connection, transaction);

            return WalletOperationResult.Ok(walletTransaction.Id);
        }

        public Task<WalletOperationResult> Execute(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<WalletOperationResult> Void(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
