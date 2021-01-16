using ExplyMe.Modules.Notification.Domain.Entities;
using ExplyMe.Modules.Notification.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Notification.Domain.Enums;

namespace ExplyMe.Modules.Notification.Services
{
    public class NotificationService : INotificationService
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public INotificationMessageCompiler MessageCompiler { get; }

        public NotificationService(
            ISqlConnectionFactory sqlConnectionFactory,
            INotificationMessageCompiler messageCompiler)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
            MessageCompiler = messageCompiler ?? throw new ArgumentNullException(nameof(messageCompiler));
        }

        public Task DeleteAsync(Guid notificationId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NotificationEntity>> FindNotificationsByUserIdAsync(long userId)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();

            const string query = @"SELECT * FROM [Notification] WHERE RecipientId = @userId";
            var result = await connection.QueryAsync<NotificationEntity>(query, new { userId });

            return result;
        }

        public async Task<NotificationEntity> GetAsync(Guid notificationId)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();

            const string query = @"SELECT * FROM [Notification] WHERE Id = @notificationId";
            var result = await connection.QueryFirstOrDefaultAsync<NotificationEntity>(query, new { notificationId });

            return result;
        }

        public async Task SendAsync(NotificationTypeEnum notificationType, long recipientId, string url, object data)
        {
            var notification = new NotificationEntity
            {
                NotificationTypeId = notificationType,
                RecipientId = recipientId,
                Url = url,
                CompiledMessage = await MessageCompiler.CompileAsync(notificationType, data)
            };

            using var connection = SqlConnectionFactory.CreateWriteConnection();

            const string query =
@"INSERT INTO [Notification]
    ( [Id], [CreatedAt], [NotificationTypeId], [RecipientId], [Url], [CompiledMessage])
VALUES
    (@Id, @CreatedAt, @NotificationTypeId, @RecipientId, @Url, @CompiledMessage)";

            await connection.ExecuteAsync(query, notification);
        }
    }
}
