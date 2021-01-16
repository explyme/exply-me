using Dapper;
using ExplyMe.Modules.Wallet.Data.Interfaces;
using ExplyMe.Modules.Wallet.Domain.Entities;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Data
{
    public class WalletTransactionStore : IWalletTransactionStore
    {
        public async Task<WalletTransaction> CreateAsync(
            WalletTransaction walletTransaction,
            SqlConnection connection,
            DbTransaction dbTransaction)
        {
            await connection.ExecuteAsync(
@"INSERT INTO [WalletTransaction] 
    ([Id], [Status], [FromId], [ToId], [CreatedAt], [ExecutedAt], [VoidedAt], [Amount], [SoftDescriptor])
VALUES
    (@Id], @Status, @FromId, @ToId, @CreatedAt, @ExecutedAt, @VoidedAt, @Amount, @SoftDescriptor])",
                    walletTransaction, transaction: dbTransaction);

            return walletTransaction;
        }
    }
}
