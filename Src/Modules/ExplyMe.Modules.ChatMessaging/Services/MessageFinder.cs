using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.ChatMessaging.Domain.Entities;
using ExplyMe.Modules.ChatMessaging.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExplyMe.Modules.ChatMessaging.Services
{
    public class MessageFinder: IMessageFinder
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public MessageFinder(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<IEnumerable<MessageEntity>> FindAllByFromUserIdAndToUserId(long fromUserId, long toUserId)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();

            const string query =
@"SELECT * FROM [Message] 
WHERE
(FromUser = @fromUserId AND ToUser = @toUserId)
OR
(FromUser = @toUserId AND ToUser = @fromUserId)";
            var result = await connection.QueryAsync<MessageEntity>(query, new { fromUserId, toUserId });

            return result;
        }
    }
}
