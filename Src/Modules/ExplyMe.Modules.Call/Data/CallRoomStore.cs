using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Call.Data.Interfaces;
using ExplyMe.Modules.Call.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Call.Data
{
    public class CallRoomStore : ICallRoomStore
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public CallRoomStore(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<CallRoom> GetAsync(Guid roomId)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();
            var query = @"SELECT * FROM [CallRoom] (NOLOCK) WHERE [CallRoom].[ExpireAt] > @utcNow AND [CallRoom].[Id] = @roomId";
            var result = await connection.QueryFirstOrDefaultAsync<CallRoom>(query, new
            {
                utcNow = DateTime.UtcNow,
                roomId
            });

            return result;
        }

        public async Task CreateRoom(CallRoom room)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();
            var query =
@"INSERT INTO [CallRoom]
    ([Id], [ExpireAt], [CreatedAt], [TwilioSid], [TwilioUniqueName])
VALUES
    (@Id, @ExpireAt, @CreatedAt, @TwilioSid, @TwilioUniqueName)";

            await connection.ExecuteAsync(query, room);
        }

        public async Task AssociateUserToRoom(Guid roomId, long userId)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();
            var query = @"INSERT INTO [CallRoomAllowedUsers] ([RoomID], [UserId]) VALUES (@roomId, @userId)";

            await connection.ExecuteAsync(query, new { roomId, userId });
        }
    }
}
