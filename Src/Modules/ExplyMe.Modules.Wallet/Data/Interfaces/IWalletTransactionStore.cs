using ExplyMe.Modules.Wallet.Domain.Entities;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Data.Interfaces
{
    public interface IWalletTransactionStore
    {
        Task<WalletTransaction> CreateAsync(WalletTransaction walletTransaction, SqlConnection connection, DbTransaction dbTransaction);
    }
}
