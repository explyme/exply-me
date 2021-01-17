using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Calendar.Domain.Entities;
using ExplyMe.Modules.Calendar.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Calendar.Store
{
    public class CalendarSlotStore : ICalendarSlotStore
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public CalendarSlotStore(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task CreateAsync(CalendarSlot slot)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            var query =
@"INSERT INTO [CalendarSlot] (
    [UserId], [StartHour], [EndHour], [RepeatOn], [StartAt]
)
OUTPUT INSERTED.ID
VALUES(
    @UserId, @StartHour, @EndHour, @RepeatOn, @StartAt
)";

            slot.Id = await connection.ExecuteScalarAsync<long>(query, slot);
        }

        public async Task<CalendarSlot> GetAsync(long slotId)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            var query =
@"SELECT 
    [Id], [UserId], [StartHour], [EndHour], [RepeatOn], [StartAt]
FROM 
    [CalendarSlot] (NOLOCK)
WHERE
    [Id] = @slotId";

            var result = await connection.QueryFirstOrDefaultAsync<CalendarSlot>(query, new { slotId });

            return result;
        }

        public async Task<IEnumerable<CalendarSlot>> FindByUser(long userId)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            var query =
@"SELECT 
    [Id], [UserId], [StartHour], [EndHour], [RepeatOn], [StartAt]
FROM 
    [CalendarSlot] (NOLOCK)
WHERE
    [UserId] = @userId";

            var result = await connection.QueryAsync<CalendarSlot>(query, new { userId });

            return result;
        }

        public Task UpdateAsync(CalendarSlot slot)
        {
            throw new NotImplementedException();
        }
    }
}
