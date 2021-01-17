using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.ChatMessaging.Domain.Entities;
using ExplyMe.Modules.ChatMessaging.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.ChatMessaging.Services
{
    public class MessageCreator : IMessageCreator
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public MessageCreator(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }
        public async Task SendAsync(MessageEntity message)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();

            const string query =
@"INSERT INTO [Message]
    ( [Id], [FromUser], [ToUser], [Message], [CreatedAt])
VALUES
    (@Id, @FromUser, @ToUser, @Message, @CreatedAt)";

            await connection.ExecuteAsync(query, message);
        }
    }
}
