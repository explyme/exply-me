using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Data.Interfaces
{
    public interface IWalletAccountBalanceStore
    {
        Task IncrementAvailableBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction);
        Task DecrementAvailableBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction);
        Task IncrementBlockedBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction);
        Task DecrementBlockedBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction);
        Task IncrementFutureBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction);
        Task DecrementFutureBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction);
    }
}
