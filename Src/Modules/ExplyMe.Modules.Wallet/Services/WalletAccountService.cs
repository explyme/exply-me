using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Wallet.Domain;
using ExplyMe.Modules.Wallet.Domain.Entities;
using ExplyMe.Modules.Wallet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Wallet.Services
{
    public class WalletAccountService : IWalletAccountService
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public WalletAccountService(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public Task<bool> AuditBalance(long accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<WalletAccount> CreateAsync(long userId)
        {
            var wallet = new WalletAccount();

            using var connection = SqlConnectionFactory.CreateWriteConnection();

            async Task CreateAccount()
            {
                var query =
@"INSERT INTO [WalletAccount]
([AvailableBalance], [BlockedBalance], [FutureBalance], [CreatedAt])
OUTPUT INSERTED.ID
VALUES(0, 0, 0, @CreatedAt)";

                wallet.Id = await connection.ExecuteScalarAsync<long>(query, wallet);
            }

            async Task AssociateUserToAccount()
            {
                var query =
@"INSERT INTO [UserWallets]
([WalletId], [UserId])
VALUES(@walletId, @userId)";

                await connection.ExecuteScalarAsync<long>(query, new
                {
                    walletId = wallet.Id,
                    userId
                });
            }

            await CreateAccount();
            await AssociateUserToAccount();

            return wallet;
        }

        public async Task<IEnumerable<WalletAccount>> FindUserAccountsAsync(long userId)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            var query =
@"SELECT * FROM [WalletAccount] (NOLOCK)
INNER JOIN [UserWallets] (NOLOCK) ON [UserWallets].WalletId = [WalletAccount].Id
WHERE [UserWallets].[UserId] = @userId";

            var result = await connection.QueryAsync<WalletAccount>(query, new { userId });

            return result;
        }

        public async Task<WalletAccount> GetAsync(long accountId)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            var query = @"SELECT * FROM [WalletAccount] (NOLOCK) WHERE [WalletAccount].[Id] = @accountId";

            var result = await connection.QueryFirstOrDefaultAsync<WalletAccount>(query, new { accountId });

            return result;
        }

        public async Task<WalletAccountBalance> GetTotalBalance(long accountId)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            var query = 
@"SELECT [AvailableBalance], [BlockedBalance], [FutureBalance] FROM [WalletAccount] (NOLOCK) WHERE [WalletAccount].[Id] = @accountId";

            var result = await connection.QueryFirstOrDefaultAsync<WalletAccountBalance>(query, new { accountId });

            return result;
        }
    }
}
