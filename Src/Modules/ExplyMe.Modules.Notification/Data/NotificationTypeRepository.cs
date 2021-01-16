using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Notification.Data.Interface;
using ExplyMe.Modules.Notification.Domain.Entities;
using ExplyMe.Modules.Notification.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Notification.Data
{
    public class NotificationTypeRepository : INotificationTypeRepository
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public NotificationTypeRepository(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<NotificationType> GetAsync(NotificationTypeEnum notificationType)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();

            const string query = @"SELECT * FROM [NotificationType] WHERE Id = @notificationType";
            var result = await connection.QueryFirstOrDefaultAsync<NotificationType>(query, new { notificationType });

            return result;
        }
    }
}
