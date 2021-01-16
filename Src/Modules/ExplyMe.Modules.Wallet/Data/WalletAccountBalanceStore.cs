using Dapper;
using ExplyMe.Modules.Wallet.Data.Interfaces;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Data
{
    public class WalletAccountBalanceStore : IWalletAccountBalanceStore
    {
        #region Available Balance

        public async Task IncrementAvailableBalanceAsync(
            long accountId, 
            long amount,
            SqlConnection connection,
            DbTransaction dbTransaction)
        {
            await UpdateBalance(
                "AvailableBalance", 
                isIncrement: true, 
                accountId: accountId, 
                amountToBeCalculated: amount,
                connection,
                dbTransaction);
        }

        public async Task DecrementAvailableBalanceAsync(
            long accountId, 
            long amount, 
            SqlConnection connection, 
            DbTransaction dbTransaction)
        {
            await UpdateBalance(
                "AvailableBalance",
                isIncrement: false,
                accountId: accountId,
                amountToBeCalculated: amount,
                connection,
                dbTransaction);
        }

        #endregion

        #region Blocked Balance

        public async Task IncrementBlockedBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction)
        {
            await UpdateBalance(
                "BlockedBalance",
                isIncrement: true,
                accountId: accountId,
                amountToBeCalculated: amount,
                connection,
                dbTransaction);
        }

        public async Task DecrementBlockedBalanceAsync(long accountId, long amount, SqlConnection connection, DbTransaction dbTransaction)
        {
            await UpdateBalance(
                "BlockedBalance",
                isIncrement: false,
                accountId: accountId,
                amountToBeCalculated: amount,
                connection,
                dbTransaction);
        }

        #endregion

        #region Future Balance

        public async Task IncrementFutureBalanceAsync(
            long accountId, 
            long amount, 
            SqlConnection connection, 
            DbTransaction dbTransaction)
        {
            await UpdateBalance(
                "FutureBalance",
                isIncrement: true,
                accountId: accountId,
                amountToBeCalculated: amount,
                connection,
                dbTransaction);
        }
        public async Task DecrementFutureBalanceAsync(
            long accountId, 
            long amount, 
            SqlConnection connection, 
            DbTransaction dbTransaction)
        {
            await UpdateBalance(
                "FutureBalance",
                isIncrement: false,
                accountId: accountId,
                amountToBeCalculated: amount,
                connection,
                dbTransaction);
        }

        #endregion

        #region Privates
        private async Task UpdateBalance(
            string balanceField,
            bool isIncrement,
            long accountId,
            long amountToBeCalculated,
            SqlConnection connection,
            DbTransaction dbTransaction)
        {
            var userBalance = await connection.ExecuteScalarAsync<long>(
                    $"SELECT TOP 1 [{balanceField}] from [WalletAccount] where [WalletAccount].Id = @accountId",
                    new { accountId }, transaction: dbTransaction);

            var newBalance = isIncrement 
                ? userBalance + amountToBeCalculated
                : userBalance - amountToBeCalculated;

            await connection.ExecuteAsync(
                    $"UPDATE [WalletAccount] SET [{balanceField}] = @newBalance where [WalletAccount].Id = @accountId",
                    new { newBalance, accountId }, transaction: dbTransaction);
        }
        #endregion
    }
}
