using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Core.Identity
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public UserStore(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query =
@"INSERT INTO [User]
    ([PasswordHash], [LockoutEnd], [LockoutEnabled], 
     [EmailConfirmed], [Email], [AccessFailedCount], [FullName], [CreatedAt])
OUTPUT INSERTED.ID
VALUES
    (@PasswordHash, @LockoutEnd, @LockoutEnabled, 
     @EmailConfirmed, @Email, @AccessFailedCount, @FullName, @CreatedAt)";
            using var connection = SqlConnectionFactory.CreateReadConnection();

            user.Id = await connection.ExecuteScalarAsync<long>(query, user);

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = "SELECT * FROM [User] WHERE Id = @userId";
            using var connection = SqlConnectionFactory.CreateReadConnection();

            var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { userId });

            return result;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = "SELECT * FROM [User] WHERE Email = @normalizedUserName";
            using var connection = SqlConnectionFactory.CreateReadConnection();

            var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { normalizedUserName });

            return result;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Email);

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.PasswordHash);

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Email);

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.PasswordHash != null);

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Email = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM [User] WHERE Email = @normalizedEmail";
            using var connection = SqlConnectionFactory.CreateReadConnection();

            // TODO: usar varchar
            var result = await connection.QueryFirstOrDefaultAsync<User>(query, new { normalizedEmail });

            return result;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Email);

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.EmailConfirmed);

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Email);

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.Email = normalizedEmail;
            return Task.CompletedTask;
        }
    }
}
